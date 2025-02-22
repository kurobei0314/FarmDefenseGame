using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public interface ISetEquipmentUseCase
    {
        IWeaponEntity PlayerCurrentWeapon { get; }
        IArmorEntity PlayerCurrentArmor { get; }
        IWeaponEntity[] HasWeaponEntity { get; }
        IArmorEntity[] HasArmorEntity { get; }
        IWeaponEntity GetWeaponEntityById(int weaponEntityId);
        IArmorEntity GetArmorEntityById(int armorEntityId);
        void SetCurrentWeapon(int weaponEntityId);
        void SetCurrentArmor(int armorEntityId);
    }
}
