using System.Collections.Generic;
using R3;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerEntity : ISetStatus
    {
        IPlayerStatusVO PlayerStatusVO { get; }
        ReactiveProperty<int> CurrentHP { get; }
        int CurrentHPValue { get; }
        int CurrentMaxHP { get; }
        IWeaponEntity SetCurrentWeapon { get; }
        ISkillEntity[] SetCurrentSkills { get; }
        void ReduceHP(int value);
        bool IsAttack();
    }
}
