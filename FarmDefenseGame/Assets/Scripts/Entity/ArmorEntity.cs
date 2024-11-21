using WolfVillageBattle.Interface;

namespace WolfVillageBattle
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