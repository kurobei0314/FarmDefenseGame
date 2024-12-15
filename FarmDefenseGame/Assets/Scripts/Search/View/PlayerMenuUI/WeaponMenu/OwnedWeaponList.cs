using System.Linq;
using WolfVillage.Common;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class OwnedWeaponList : ScrollList<OwnedWeaponPanel, OwnedWeaponPanelVM>
    {
        public void Initialize(IWeaponEntity setWeaponEntity, IWeaponEntity[] ownedWeaponEntities)
        {
            base.Initialize(ownedWeaponEntities.Select(entity => new OwnedWeaponPanelVM(entity, entity.Id == setWeaponEntity.Id)).ToArray(), null);
        }
    }
}
