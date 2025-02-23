using R3;

namespace WolfVillage.Entity.Interface
{
    public interface IBattlePlayerEntity : ISetStatus, IPlayerEntity
    {
        ReactiveProperty<int> CurrentHP { get; }
        int CurrentHPValue { get; }
        ISkillEntity[] CurrentWeaponTypeSkills { get; }
        void ReduceHP(int value);
        bool IsAttack();
    }
}
