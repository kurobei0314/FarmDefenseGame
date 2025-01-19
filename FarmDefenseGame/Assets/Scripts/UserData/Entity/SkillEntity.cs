using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity
{
    public class SkillEntity : ISkillEntity
    {
        public enum Status
        {
            CanUse,
            IntervalTime,
        }

        public SkillEntity(int id, ISkillVO skillVO)
        {
            this.id = id;
            this.skillVO = skillVO;
            current_status = Status.CanUse;
        }

        private int id;
        public int Id => id;
        private ISkillVO skillVO;
        public ISkillVO SkillVO => skillVO;
        private Status current_status;

        public bool AbleUseSkill()
            => current_status == Status.CanUse;
        
        public void UpdateStatus(Status status)
            => current_status = status;

    }
}
