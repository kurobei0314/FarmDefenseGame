using WolfVillage.Common;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class OwnedSkillListPanelVM : ScrollPanelVM
    {
        private ISkillEntity _entity;
        public OwnedSkillListPanelVM(int id, ISkillEntity entity) : base(id)
        {
            _entity = entity;
        }
        public ISkillEntity SkillEntity => _entity;
    }
}
