using System.Collections.Generic;
using R3;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerEntity : ISetStatus
    {
        IPlayerVO PlayerVO { get; }
        ReactiveProperty<int> CurrentHP { get; }
        int CurrentHPValue { get; }
        IWeaponEntity SetCurrentWeapon { get; }
        ISkillEntity[] SetCurrentSkills { get; }
        void ReduceHP(int value);
        bool IsAttack();
    }
}
