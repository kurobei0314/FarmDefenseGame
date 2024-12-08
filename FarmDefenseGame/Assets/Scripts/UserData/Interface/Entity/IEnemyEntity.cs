using R3;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity.Interface
{
    public interface IEnemyEntity : ISetStatus
    {
        IEnemyVO EnemyVO { get; }
        ReactiveProperty<int> CurrentHP { get; }
        int CurrentHPValue { get; }
        public void ReduceHP(int value);
    }
}

