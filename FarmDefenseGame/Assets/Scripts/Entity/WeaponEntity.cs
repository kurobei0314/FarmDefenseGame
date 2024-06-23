using WolfVillageBattle.Interface;

namespace WolfVillageBattle
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
