using WolfVillage.Common;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class OwnedSkillListPanelVM : ScrollPanelVM
    {
        private ISkillEntity _entity;
        private bool _isSet;
        public OwnedSkillListPanelVM(int id, ISkillEntity entity, bool isSet) : base(id)
        {
            _entity = entity;
            _isSet = isSet;
        }
        public ISkillEntity SkillEntity => _entity;
        public bool IsSet => _isSet;
    }
}
