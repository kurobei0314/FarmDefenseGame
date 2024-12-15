using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity
{
    public class WeaponEntity : IWeaponEntity
    {
        private int id;
        private IWeaponVO weaponVO;

        public WeaponEntity(int id, IWeaponVO weaponVO)
        {
            this.id = id;
            this.weaponVO = weaponVO;
        }
        public int Id => id;
        public IWeaponVO WeaponVO => weaponVO;
    }
}
