using R3;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity.Interface
{
    public interface IPlayerEntity : ISetStatus
    {
        IPlayerStatusVO PlayerStatusVO { get; }
        ReactiveProperty<int> CurrentHP { get; }
        int CurrentHPValue { get; }
        int CurrentMaxHP { get; }
        IWeaponEntity SetCurrentWeapon { get; }
        IArmorEntity SetCurrentArmor { get; }
        ISkillEntity[] SetCurrentSkills { get; }
        void ReduceHP(int value);
        bool IsAttack();
    }
}
