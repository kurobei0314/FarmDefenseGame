using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class WeaponMenuVM 
    {
        public enum FocusWeaponMenuUIState
        {
            SetWeaponPanel,
            SetArmorPanel,
            OwnedWeaponList,
            OwnedArmorList,
        }
        private FocusWeaponMenuUIState _state;
        public FocusWeaponMenuUIState State => _state;

        private IWeaponEntity _setWeaponEntity;
        private IArmorEntity _setArmorEntity;

        public WeaponMenuVM(IWeaponEntity setWeaponEntity, IArmorEntity setArmorEntity)
        {
            _setWeaponEntity = setWeaponEntity;
            _setArmorEntity = setArmorEntity;
            SetState(FocusWeaponMenuUIState.SetWeaponPanel);
        }

        public void SetState(FocusWeaponMenuUIState state)
            => _state = state;
    }
}