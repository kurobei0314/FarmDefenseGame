using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity.Interface
{
    public interface IArmorEntity
    {
        int Id { get; }
        IArmorVO ArmorVO { get; }
    }
}
