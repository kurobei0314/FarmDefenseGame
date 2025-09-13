using WolfVillage.Common;
using WolfVillage.Entity.Interface;
using WolfVillage.Search.PlayerMenuUI;

namespace WolfVillage.Search
{
    public class OpenPlayerMenuController
    {
        IOpenPlayerMenuUseCase _openPlayerMenuUseCase;
        public OpenPlayerMenuController(IInputController inputController, IPlayerMenuUI playerMenuUI)
        {
            _openPlayerMenuUseCase = new OpenPlayerMenuActor(inputController, playerMenuUI);
        }

        public void Input(  IPlayerEntity player,
                            IWeaponEntity[] weaponEntities,
                            IArmorEntity[] armorEntities,
                            ISkillEntity[] skillEntities)
            => _openPlayerMenuUseCase.OpenPlayerMenu(player, weaponEntities, armorEntities, skillEntities);
    }
}
