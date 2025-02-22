using UnityEngine;
using WolfVillage.MasterDataStore;
using WolfVillage.Entity;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using WolfVillage.Battle;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class PlayerMenuUI : MonoBehaviour
    {
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
            var skillEntities = setSkillVO.Select((skillVO, index) => new SkillEntity(index, skillVO)).ToArray();

            var setWeaponVO = weaponVODataStore.Items.Where(vo => vo.Name != string.Empty).ToArray();
            _weaponEntities = setWeaponVO.Select((vo, index) => new WeaponEntity(index, vo)).ToArray();

            var setArmorVO = armorVODataStore.Items.Where(vo => vo.Name != string.Empty).ToArray();
            _armorEntities = setArmorVO.Select((vo, index) => new ArmorEntity(index, vo)).ToArray();
            player = new SearchPlayerEntity(playerStatusVO, skillEntities, _weaponEntities.FirstOrDefault(), _armorEntities.FirstOrDefault());

            // TODO: ここの処理も別の場所に書く
            var equipmentActor = new SetEquipmentActor(player, _weaponEntities, _armorEntities);

            Initialize(equipmentActor);
        }

        public void Initialize(ISetEquipmentUseCase equipmentUseCase)
        {
            _playerMenuVM = new PlayerMenuUIVM();
            _contentUI.Initialize(_playerMenuVM.State, equipmentUseCase);
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
        {
            _contentUI.InputStickEvent(context);
        }
        public void InputDecideEvent(InputAction.CallbackContext context)
        {
            _contentUI.InputDecideEvent(context);
        }
        public void InputCancelEvent(InputAction.CallbackContext context)
        {
            _contentUI.InputCancelEvent(context);
        }
        #endregion

        public void Dispose()
        {
            _contentUI.Dispose();
        }
    }
}
