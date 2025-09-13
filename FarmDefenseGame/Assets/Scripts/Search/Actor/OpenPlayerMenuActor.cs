using WolfVillage.Common;
using WolfVillage.Entity.Interface;
using WolfVillage.Search.PlayerMenuUI;

namespace WolfVillage.Search
{
    public class OpenPlayerMenuActor : IOpenPlayerMenuUseCase
    {
        private IInputController _inputController;
        private IPlayerMenuUI _playerMenuUI;

        public OpenPlayerMenuActor(IInputController inputController, IPlayerMenuUI playerMenuUI)
        {
            _inputController = inputController;
            _playerMenuUI = playerMenuUI;
        }

        public void OpenPlayerMenu( IPlayerEntity player,
                                    IWeaponEntity[] weaponEntities,
                                    IArmorEntity[] armorEntities,
                                    ISkillEntity[] skillEntities)
        {
            _inputController.SwitchActionMaps(ActionMapName.PlayerMenuUI);
            _playerMenuUI.Open(player, weaponEntities, armorEntities, skillEntities, () =>
            {
                _inputController.SwitchActionMaps(ActionMapName.SearchMap);
            });
        }
    }
}
