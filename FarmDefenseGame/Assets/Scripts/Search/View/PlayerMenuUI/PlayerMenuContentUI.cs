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
                                PlayerInput playerInput,
                                ISetEquipmentUseCase equipmentUseCase)
        {
            _weaponMenuUI.Initialize(playerEntity.CurrentWeapon, playerEntity.CurrentArmor, weaponEntities, armorEntities, playerInput, equipmentUseCase);
        }
    }
}
