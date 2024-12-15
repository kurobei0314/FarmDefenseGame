using UnityEngine;
using WolfVillage.Entity.Interface;

namespace  WolfVillage.Search.PlayerMenuUI
{
    public class SetCurrentWeaponPanel : MonoBehaviour
    {
        [SerializeField] private HasWeaponPanel _weaponPanel;

        public void Initialize(IWeaponEntity SetCurrentWeapon, IArmorEntity SetCurrentArmor)
        {
            var weaponVO = SetCurrentWeapon.WeaponVO;
            _weaponPanel.Initialize(weaponVO.Name, weaponVO.RoleType, weaponVO.AttackType);
        }
    }
}
