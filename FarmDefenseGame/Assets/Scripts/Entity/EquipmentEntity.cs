using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EquipmentEntity : IEquipmentEntity
    {
        public EquipmentEntity(IEquipmentVO equipmentVO)
        {
            this.equipmentVO = equipmentVO;
        }

        private IEquipmentVO equipmentVO;
        public IEquipmentVO EquipmentVO => equipmentVO;
    }
}