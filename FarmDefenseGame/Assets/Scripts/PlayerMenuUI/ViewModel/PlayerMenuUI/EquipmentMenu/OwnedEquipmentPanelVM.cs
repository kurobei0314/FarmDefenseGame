using WolfVillage.Common;

namespace WolfVillage.Search.PlayerMenuUI.EquipmentMenu
{
    public class OwnedEquipmentPanelVM : ScrollPanelVM
    {
        private string _name;
        private bool _isSet;
        private SetEquipmentChangeStatusVM _statusPanelVM;
        public OwnedEquipmentPanelVM(int id, string name, int addAttack, int addDefense, bool isSet) : base(id)
        {
            _name = name;
            _isSet = isSet;
            _statusPanelVM = new SetEquipmentChangeStatusVM(addAttack, addDefense);
        }
        public string Name => _name;
        public bool isSet => _isSet;
        public SetEquipmentChangeStatusVM StatusPanelVM => _statusPanelVM;
    }
}
