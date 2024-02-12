using UniRx;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerEntity : ISetStatus
    {
        IPlayerVO PlayerVO { get; }
        ReactiveProperty<int> CurrentHP { get; }
        int CurrentHPValue { get; }
        int Attack { get; }
        string Weapon { get; }
        void ReduceHP(int value);
    }
}
