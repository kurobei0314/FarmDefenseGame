using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class SkillEntity : ISkillEntity
    {
        public SkillEntity(ISkillVO skillVO)
        {
            this.skillVO = skillVO;
        }

        private ISkillVO skillVO;
        public ISkillVO SkillVO => skillVO;
    }
}
