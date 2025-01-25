using WolfVillage.Common;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class OwnedEquipmentPanelVM : ScrollPanelVM
    {
        private string _name;
        private bool _isSet;
        private SetEquipmentChangeStatusPanelVM _statusPanelVM;
        public OwnedEquipmentPanelVM(int id, string name, bool isSet) : base(id)
        {
            _name = name;
            _isSet = isSet;
        }
        public string Name => _name;
        public bool isSet => _isSet;
        public SetEquipmentChangeStatusPanelVM StatusPanelVM => _statusPanelVM;
    }
}
