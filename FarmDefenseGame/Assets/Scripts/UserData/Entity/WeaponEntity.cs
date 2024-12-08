using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity
{
    public class WeaponEntity : IWeaponEntity
    {
        public WeaponEntity(IWeaponVO weaponVO)
        {
            this.weaponVO = weaponVO;
        }

        private IWeaponVO weaponVO;
        public IWeaponVO WeaponVO => weaponVO;
    }
}
