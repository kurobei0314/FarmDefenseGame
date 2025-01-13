using UnityEngine;
using WolfVillage.Entity.Interface;

namespace  WolfVillage.Search.PlayerMenuUI
{
    public class SetCurrentWeaponPanel : MonoBehaviour
    {
        [SerializeField] private HasWeaponPanel _weaponPanel;
        [SerializeField] private HasWeaponPanel _armorPanel;

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
