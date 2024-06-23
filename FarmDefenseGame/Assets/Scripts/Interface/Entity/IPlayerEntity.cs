using System.Collections.Generic;
using UniRx;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerEntity : ISetStatus
    {
        IPlayerVO PlayerVO { get; }
        ReactiveProperty<int> CurrentHP { get; }
        int CurrentHPValue { get; }
        int Attack { get; }
        IWeaponEntity SetCurrentWeapon { get; }
        IReadOnlyList<ISkillEntity> SetCurrentSkills { get; }
        void ReduceHP(int value);
        bool IsAttack();
    }
}
