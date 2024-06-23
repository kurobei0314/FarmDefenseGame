namespace WolfVillageBattle.Interface
{
    public interface ISkillVO
    {
        int Id { get; }
        string Name { get; }
        RoleType RoleType { get; }
        AttackType AttackType { get; }
        float IntervalTime { get; }
        string IconImageName { get; }
    }
}
