using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity.Interface
{
    public interface ISkillEntity
    {
        int Id { get; }
        ISkillVO SkillVO { get; }
        public bool AbleUseSkill();
        public void UpdateStatus(SkillEntity.Status status);
    }
}
