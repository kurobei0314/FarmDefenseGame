using UnityEngine;
using WolfVillage.Entity.Interface;

namespace  WolfVillage.Search.PlayerMenuUI
{
    public class SetCurrentEquipmentPanel : MonoBehaviour
    {
        [SerializeField] private HasEquipmentPanel _weaponPanel;
        [SerializeField] private HasEquipmentPanel _armorPanel;

        public void Initialize(IWeaponEntity SetCurrentWeapon, IArmorEntity SetCurrentArmor)
        {
            _weaponPanel.Initialize(SetCurrentWeapon.WeaponVO.Name);
            _armorPanel.Initialize(SetCurrentArmor.ArmorVO.Name);
            SelectWeaponPanel();
        }

        public void SelectWeaponPanel()
        {
            _weaponPanel.SetSelectedPanel();
            _armorPanel.SetUnSelectedPanel();
        }

        public void SelectArmorPanel()
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
