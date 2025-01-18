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
            var weaponVO = SetCurrentWeapon.WeaponVO;
            _weaponPanel.Initialize(weaponVO.Name, weaponVO.RoleType, weaponVO.AttackType);
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
    }
}
