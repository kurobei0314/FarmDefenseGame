using WolfVillageBattle.Interface;

namespace WolfVillage.ValueObject.Interface
{
    public interface IEnemyVO 
    {
        string Name { get; }
        int Id { get; }
        int MaxHP { get; }
        int Attack { get; }
        string PrefabName { get; }
        AttackType AttackType { get; }
        string Description { get; }
    }

}
