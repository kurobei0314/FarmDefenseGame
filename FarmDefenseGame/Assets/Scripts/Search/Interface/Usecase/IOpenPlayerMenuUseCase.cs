using WolfVillage.Entity.Interface;

namespace WolfVillage.Search
{
    public interface IOpenPlayerMenuUseCase 
    {
        public void OpenPlayerMenu( IPlayerEntity player,
                                    IWeaponEntity[] weaponEntities,
                                    IArmorEntity[] armorEntities,
                                    ISkillEntity[] skillEntities);
    }
}
