using WolfVillage.Common;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class OwnedWeaponPanelVM : ScrollPanelVM
    {
        private IWeaponEntity _weaponEntity;
        private bool _isSet;
        public OwnedWeaponPanelVM(IWeaponEntity weaponEntity, bool isSet)
        {
            _weaponEntity = weaponEntity;
            _isSet = isSet;
        }
        public IWeaponEntity weaponEntity => _weaponEntity;
        public bool isSet => _isSet;
    }
}
