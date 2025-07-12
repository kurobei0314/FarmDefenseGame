using WolfVillage.Interface;
namespace WolfVillage.Entity.Interface
{
    public interface ISetEquipmentEntity : IPlayerEntity
    {
        void SetCurrentWeapon(IWeaponEntity weaponEntity);
        void SetCurrentArmor(IArmorEntity armorEntity);
    }

    public interface ISetSkillEntity : IPlayerEntity
    {
        void SetCurrentSkill(ISkillEntity skillEntity, RoleType type, int index);
    }
}
