using WolfVillageBattle;

namespace WolfVillageBattle.Interface
{
    public interface ISkillEntity
    {
        ISkillVO SkillVO { get; }
        public bool AbleUseSkill();
        public void UpdateStatus(SkillEntity.Status status);
    }
}
