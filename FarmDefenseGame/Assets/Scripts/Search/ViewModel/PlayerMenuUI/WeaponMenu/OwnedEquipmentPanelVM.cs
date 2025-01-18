using WolfVillage.Common;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class OwnedEquipmentPanelVM : ScrollPanelVM
    {
        private IWeaponEntity _weaponEntity;
        private bool _isSet;
        public OwnedEquipmentPanelVM(IWeaponEntity weaponEntity, bool isSet)
        {
            _weaponEntity = weaponEntity;
            _isSet = isSet;
        }
        public IWeaponEntity weaponEntity => _weaponEntity;
        public bool isSet => _isSet;
    }
}
