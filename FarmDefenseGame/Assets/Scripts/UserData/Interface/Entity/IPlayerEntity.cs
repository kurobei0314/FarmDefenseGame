namespace WolfVillage.Entity.Interface
{
    public interface IPlayerEntity
    {
        int CurrentMaxHP { get; }
        IWeaponEntity CurrentWeapon { get; }
        IArmorEntity CurrentArmor { get; }
        ISkillEntity[] SetCurrentSkills { get; }
    }
}
