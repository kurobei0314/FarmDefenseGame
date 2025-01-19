using WolfVillage.Common;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class OwnedEquipmentPanelVM : ScrollPanelVM
    {
        private IWeaponEntity _weaponEntity;
        private bool _isSet;
        public OwnedEquipmentPanelVM(int id, IWeaponEntity weaponEntity, bool isSet) : base(id)
        {
            _weaponEntity = weaponEntity;
            _isSet = isSet;
        }
        public IWeaponEntity weaponEntity => _weaponEntity;
        public bool isSet => _isSet;
    }
}
