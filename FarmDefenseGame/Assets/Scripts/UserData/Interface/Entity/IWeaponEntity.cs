using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity.Interface
{
    public interface IWeaponEntity
    {
        int Id { get; }
        IWeaponVO WeaponVO { get; }
    }
}
