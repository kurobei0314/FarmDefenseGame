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
        IWeaponEntity CurrentWeapon { get; }
        IArmorEntity CurrentArmor { get; }
        ISkillEntity[] SetCurrentSkills { get; }
        void ReduceHP(int value);
        void SetCurrentWeapon(IWeaponEntity weaponEntity);
        void SetCurrentArmor(IArmorEntity armorEntity);
        bool IsAttack();
    }
}
