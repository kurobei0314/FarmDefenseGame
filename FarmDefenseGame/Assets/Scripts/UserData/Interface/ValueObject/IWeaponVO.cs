using WolfVillageBattle.Interface;

namespace WolfVillage.ValueObject.Interface
{
    public interface IWeaponVO
    {
        string Name { get; }
        int Id { get; }
        int Attack { get; }
        RoleType RoleType { get; }
        AttackType AttackType { get; }
        string Description { get; }
    }
}
