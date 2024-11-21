namespace WolfVillageBattle.Interface
{
    public interface IWeaponVO
    {
        string Name { get; }
        int Id { get; }
        int Attack { get; }
        RoleType RoleType { get; }
        AttackType AttackType { get; }
    }
}
