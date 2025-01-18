using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class PlayerMenuContentUI : MonoBehaviour
    {
        [SerializeField] private EquipmentMenuUI _weaponMenuUI;
        public void Initialize(IPlayerEntity playerEntity,
                                IWeaponEntity[] weaponEntities,
                                IArmorEntity[] armorEntities,
                                PlayerInput playerInput)
        {
            _weaponMenuUI.Initialize(playerEntity.SetCurrentWeapon, playerEntity.SetCurrentArmor, weaponEntities, armorEntities, playerInput);
        }
    }
}
