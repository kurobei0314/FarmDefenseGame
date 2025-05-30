using System.Collections.Generic;
using WolfVillage.Entity.Interface;
using WolfVillage.Interface;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity
{
    public class SearchPlayerEntity : PlayerEntity, ISearchPlayerEntity
    {
        public SearchPlayerEntity ( IPlayerStatusVO playerStatusVO,
                                    Dictionary<RoleType, ISkillEntity[]> skillEntities,
                                    IWeaponEntity weaponEntity,
                                    IArmorEntity armorEntity) : base( playerStatusVO, skillEntities, weaponEntity, armorEntity)
        {
            
        }

        public void SetCurrentWeapon(IWeaponEntity weaponEntity)
            => setCurrentWeapon = weaponEntity;

        public void SetCurrentArmor(IArmorEntity armorEntity)
            => setCurrentArmor = armorEntity;
    }
}
