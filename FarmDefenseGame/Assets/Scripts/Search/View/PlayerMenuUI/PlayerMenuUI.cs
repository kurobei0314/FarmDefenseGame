using UnityEngine;
using WolfVillage.MasterDataStore;
using WolfVillage.Entity;
using System.Linq;
using WolfVillage.Entity.Interface;
using UnityEngine.InputSystem;

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
        [SerializeField] private PlayerInput _playerInput;
        private IPlayerEntity player;
        private WeaponEntity[] _weaponEntities;
        private ArmorEntity[] _armorEntities;
        // TODO: masterやユーザーデータの情報は、ここで取得するわけでなく別のところに持たせるようにする
        void Start()
        {
            var playerStatusVO = playerDataStore.Items.FirstOrDefault();
            
            var setSkillVO = skillVODataStore.Items.Where(skillVO => skillVO.Id == 1).ToArray();
            var skillEntities = setSkillVO.Select(skillVO => new SkillEntity(skillVO)).ToArray();

            var setWeaponVO = weaponVODataStore.Items.Where(vo => vo.Name != string.Empty).ToArray();
            _weaponEntities = setWeaponVO.Select(vo => new WeaponEntity(1, vo)).ToArray();

            var setArmorVO = armorVODataStore.Items.Where(vo => vo.Id == 1).ToArray();
            _armorEntities = setArmorVO.Select(vo => new ArmorEntity(vo)).ToArray();
            player = new PlayerEntity(playerStatusVO, skillEntities, _weaponEntities.FirstOrDefault(), _armorEntities.FirstOrDefault());

            Initialize();
        }

        public void Initialize()
        {
            _contentUI.Initialize(player, _weaponEntities, _armorEntities, _playerInput);
        }
    }
}
