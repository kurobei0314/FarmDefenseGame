using UnityEngine;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class WeaponMenuUI : MonoBehaviour
    {
        [SerializeField] private OwnedWeaponList _ownedWeaponList;
        [SerializeField] private SetCurrentWeaponPanel _currentWeaponPanel;

        public void Initialize( IWeaponEntity setWeaponEntity,
                                IArmorEntity setArmorEntity, 
                                IWeaponEntity[] ownedWeaponEntities,
                                IArmorEntity[] ownedArmorEntities)
        {
            _currentWeaponPanel.Initialize(setWeaponEntity, setArmorEntity);
            _ownedWeaponList.Initialize(setWeaponEntity, ownedWeaponEntities);
        }

    }
}
