using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity
{
    public class ArmorEntity : IArmorEntity
    {
        public ArmorEntity(IArmorVO armorVO)
        {
            this.armorVO = armorVO;
        }

        private IArmorVO armorVO;
        public IArmorVO ArmorVO => armorVO;
    }
}