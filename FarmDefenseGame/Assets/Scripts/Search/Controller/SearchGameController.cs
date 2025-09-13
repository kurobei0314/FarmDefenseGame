using UnityEngine;
using WolfVillage.MasterDataStore;
using System.Linq;
using System.Collections.Generic;
using WolfVillage.Interface;
using WolfVillage.Entity.Interface;
using System;
using WolfVillage.Entity;
using UnityEngine.SceneManagement;
using WolfVillage.Battle;
using UnityEngine.InputSystem;
using WolfVillage.Search.PlayerMenuUI;
using WolfVillage.Common;

namespace WolfVillage.Search
{
    public class SearchGameController : MonoBehaviour
    {
        [SerializeField] private PlayerMenu _playerMenuUI;
        [SerializeField] private SearchCameraView _searchCameraView;

        // あとでシーンごとに設定しないようにするように変えたい
        [SerializeField] private InputController _inputController;
        // TODO: 仮
        [SerializeField] private WeaponVODataStore weaponVODataStore;
        [SerializeField] private SkillVODataStore skillVODataStore;
        [SerializeField] private ArmorVODataStore armorVODataStore;
        [SerializeField] private PlayerStatusVODataStore playerDataStore;

        [SerializeField] private MoveController _moveController;
        
        private SearchPlayerEntity player;
        private WeaponEntity[] _weaponEntities;
        private ArmorEntity[] _armorEntities;


        void Start()
        {
            _inputController.Initialize(ActionMapName.SearchMap);
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

            _moveController.Initialize(_searchCameraView, _inputController);
        }

        #region InputSystemEventHandler
        public void InputDebugBattleStartEvent(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            SceneManager.sceneLoaded += BattleSceneLoaded;
            SceneManager.LoadScene("Main");
        }
        private void BattleSceneLoaded(Scene next, LoadSceneMode mode)
        {
            var controller = GameObject.Find("GameController").GetComponent<MainGameController>();
            controller.Initialize(player);
            SceneManager.sceneLoaded -= BattleSceneLoaded;
        }
        #endregion InputSystemEventHandler

    }
}
