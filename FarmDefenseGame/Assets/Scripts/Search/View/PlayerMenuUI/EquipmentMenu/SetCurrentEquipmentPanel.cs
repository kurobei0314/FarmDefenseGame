using UnityEngine;
using WolfVillage.Entity.Interface;

namespace  WolfVillage.Search.PlayerMenuUI
{
    public class SetCurrentEquipmentPanel : MonoBehaviour
    {
        [SerializeField] private HasEquipmentPanel _weaponPanel;
        [SerializeField] private HasEquipmentPanel _armorPanel;
        [SerializeField] private PlayerStatusChangerUI _statusPanel;

        public void Initialize(IWeaponEntity SetCurrentWeapon, IArmorEntity SetCurrentArmor)
        {
            SetEquipments(SetCurrentWeapon, SetCurrentArmor);
            FocusWeaponPanel();
        }

        public void SetEquipments(IWeaponEntity SetCurrentWeapon, IArmorEntity SetCurrentArmor)
        {
            _statusPanel.Initialize(SetCurrentWeapon.WeaponVO.Attack, SetCurrentArmor.ArmorVO.Defense);
            UpdateHasEquipmentPanelViews(SetCurrentWeapon, SetCurrentArmor);
        }

        private void UpdateHasEquipmentPanelViews(IWeaponEntity SetCurrentWeapon, IArmorEntity SetCurrentArmor)
        {
            _weaponPanel.Initialize(SetCurrentWeapon.WeaponVO.Name);
            _armorPanel.Initialize(SetCurrentArmor.ArmorVO.Name);
        }

        public void SetEquipmentChangeStatus(SetEquipmentChangeStatusVM equipmentChangeStatus)
            => _statusPanel.SetEquipmentChangeStatus(equipmentChangeStatus);

        public void FocusWeaponPanel()
        {
            _weaponPanel.SetSelectedPanel();
            _armorPanel.SetUnSelectedPanel();
        }

        public void FocusArmorPanel()
        {
            _weaponPanel.SetUnSelectedPanel();
            _armorPanel.SetSelectedPanel();
        }
        
        public void Dispose()
        {
            _weaponPanel.Dispose();
            _armorPanel.Dispose();
        }
    }
}
