namespace WolfVillage.Entity.Interface
{
    public interface ISearchPlayerEntity : IPlayerEntity
    {
        void SetCurrentWeapon(IWeaponEntity weaponEntity);
        void SetCurrentArmor(IArmorEntity armorEntity);
    }
}
