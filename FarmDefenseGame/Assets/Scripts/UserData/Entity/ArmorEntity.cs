using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity
{
    public class ArmorEntity : IArmorEntity
    {
        private int _id;
        private IArmorVO _armorVO;
        public ArmorEntity(int id, IArmorVO armorVO)
        {
            _armorVO = armorVO;
            _id = id;
        }
        public int Id => _id;
        public IArmorVO ArmorVO => _armorVO;
    }
}