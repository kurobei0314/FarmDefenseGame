using UnityEngine;
using WolfVillage.MasterDataStore;
using WolfVillage.Entity;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using WolfVillage.Battle;
using WolfVillage.Search.PlayerMenuUI.EquipmentMenu;
using WolfVillage.Search.PlayerMenuUI.SkillMenu;
using System.Collections.Generic;
using WolfVillage.Interface;
using WolfVillage.Entity.Interface;
using System;
using Extension;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class PlayerMenuUI : MonoBehaviour
    {
        [SerializeField] private PlayerMenuHeaderUI _headerUI;
        [SerializeField] private PlayerMenuContentUI _contentUI;

        // TODO: 仮
        [SerializeField] private WeaponVODataStore weaponVODataStore;
        [SerializeField] private SkillVODataStore skillVODataStore;
        [SerializeField] private ArmorVODataStore armorVODataStore;
        [SerializeField] private PlayerStatusVODataStore playerDataStore;
        private SearchPlayerEntity player;
        private WeaponEntity[] _weaponEntities;
        private ArmorEntity[] _armorEntities;
        private PlayerMenuUIVM _playerMenuVM;

        // TODO: masterやユーザーデータの情報は、ここで取得するわけでなく別のところに持たせるようにする
        void Start()
        {
            var playerStatusVO = playerDataStore.Items.FirstOrDefault();
            
            var setSkillVO = skillVODataStore.Items.Where(skillVO => skillVO.Id == 1).ToArray();
            var skillEntities = skillVODataStore.Items.Select((skillVO, index) => new SkillEntity(index, skillVO)).ToArray();
            Dictionary<RoleType, ISkillEntity[]> setAllTypeSkillEntities = new Dictionary<RoleType, ISkillEntity[]>();
            foreach ( var type in Enum.GetValues(typeof(RoleType)))
            {
                var roleType = (RoleType)type;
                if (skillEntities[0].SkillVO.RoleType == roleType)
                {
                    setAllTypeSkillEntities.Add(roleType, new ISkillEntity[GameInfo.PLAYER_SET_SKILL_NUM]);
                    setAllTypeSkillEntities[roleType][0] = skillEntities[0];
                }
                else
                {
                    setAllTypeSkillEntities.Add(roleType, new ISkillEntity[GameInfo.PLAYER_SET_SKILL_NUM]);
                }
            }

            var setWeaponVO = weaponVODataStore.Items.Where(vo => vo.Name != string.Empty).ToArray();
            _weaponEntities = setWeaponVO.Select((vo, index) => new WeaponEntity(index, vo)).ToArray();

            var setArmorVO = armorVODataStore.Items.Where(vo => vo.Name != string.Empty).ToArray();
            _armorEntities = setArmorVO.Select((vo, index) => new ArmorEntity(index, vo)).ToArray();
            player = new SearchPlayerEntity(playerStatusVO, setAllTypeSkillEntities, _weaponEntities.FirstOrDefault(), _armorEntities.FirstOrDefault());

            // TODO: ここの処理も別の場所に書く
            var equipmentActor = new SetEquipmentActor(player, _weaponEntities, _armorEntities);
            var skillActor = new SetSkillActor(player, skillEntities);

            Initialize(equipmentActor, skillActor);
        }

        public void Initialize( ISetEquipmentUseCase equipmentUseCase,
                                ISetSkillUseCase skillUseCase)
        {
            _playerMenuVM = new PlayerMenuUIVM();
            _contentUI.Initialize(_playerMenuVM.State, equipmentUseCase, skillUseCase);
            _headerUI.UpdateView(_playerMenuVM.State);
        }

        // TODO: デバックのためなので後で消す
        #region InputSystemEventHandler
        public void InputDebugBattleStartEvent(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            SceneManager.sceneLoaded += BattleSceneLoaded;
            SceneManager.LoadScene("Main");
        }

        // TODO: デバックのためなので後で消す
        private void BattleSceneLoaded(Scene next, LoadSceneMode mode)
        {
            var controller = GameObject.Find("GameController").GetComponent<MainGameController>();
            controller.Initialize(player);
            SceneManager.sceneLoaded -= BattleSceneLoaded;
        }

        // TODO: 絶対にUI全体を管理するクラスを作成し、そこで通知を受け取るようにする
        public void InputStickEvent(InputAction.CallbackContext context)
            => _contentUI.InputStickEvent(context);
        public void InputDecideEvent(InputAction.CallbackContext context)
            => _contentUI.InputDecideEvent(context);
        public void InputCancelEvent(InputAction.CallbackContext context)
            => _contentUI.InputCancelEvent(context);
        public void InputSwitchPreCategoryEvent(InputAction.CallbackContext context)
        => UpdatePlayerMenuContentView(context, -1);
        public void InputSwitchNextCategoryEvent(InputAction.CallbackContext context)
        => UpdatePlayerMenuContentView(context, 1);
        public void InputSwitchPreSubCategoryEvent(InputAction.CallbackContext context)
            => _contentUI.InputSwitchSubCategoryEvent(context, -1);
        public void InputSwitchNextSubCategoryEvent(InputAction.CallbackContext context)
            => _contentUI.InputSwitchSubCategoryEvent(context, 1);
        #endregion

        private void UpdatePlayerMenuContentView(InputAction.CallbackContext context, int index)
        {
            if (!context.started) return;
            var nextIndex = (int)_playerMenuVM.State + index;
            if (nextIndex < EnumHelper.GetMinIndexEnum<PlayerMenuState>())
                nextIndex = EnumHelper.GetMaxIndexEnum<PlayerMenuState>();
            else if (EnumHelper.GetMaxIndexEnum<PlayerMenuState>() < nextIndex)
                nextIndex = EnumHelper.GetMinIndexEnum<PlayerMenuState>();
            
            _playerMenuVM.SetPlayerMenuState((PlayerMenuState)nextIndex);
            _contentUI.UpdateView(_playerMenuVM.State);
            _headerUI.UpdateView(_playerMenuVM.State);
        }

        public void Dispose()
        {
            _headerUI.Dispose();
            _contentUI.Dispose();
        }
    }
}
