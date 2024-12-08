using WolfVillageBattle.Interface;

namespace WolfVillage.ValueObject.Interface
{
    public interface ISkillVO
    {
        int Id { get; }
        string Name { get; }
        RoleType RoleType { get; }
        AttackType AttackType { get; }
        float IntervalTime { get; }
        string IconImageName { get; }
        string Description { get; }
    }
}
