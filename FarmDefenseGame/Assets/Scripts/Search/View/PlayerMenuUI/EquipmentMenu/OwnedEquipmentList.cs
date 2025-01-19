using System.Linq;
using WolfVillage.Common;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class OwnedEquipmentList : VerticalScrollList<OwnedEquipmentPanel, OwnedEquipmentPanelVM>
    {
        public void Initialize(IWeaponEntity setWeaponEntity, IWeaponEntity[] ownedWeaponEntities)
        {
            base.Initialize(ownedWeaponEntities.Select(entity => new OwnedEquipmentPanelVM(entity.Id, entity, entity.Id == setWeaponEntity.Id)).ToArray(), null);
        }
    }
}
