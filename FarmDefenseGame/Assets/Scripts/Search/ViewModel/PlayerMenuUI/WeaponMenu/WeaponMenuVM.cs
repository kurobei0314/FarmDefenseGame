using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class WeaponMenuVM 
    {
        public enum WeaponMenuUIState
        {
            SetWeaponPanel,
            SetArmorPanel,
            OwnedWeaponList,
            OwnedArmorList,
        }
        private WeaponMenuUIState _state;
        public WeaponMenuUIState State => _state;

        private IWeaponEntity _setWeaponEntity;
        private IArmorEntity _setArmorEntity;

        public WeaponMenuVM(IWeaponEntity setWeaponEntity, IArmorEntity setArmorEntity)
        {
            _setWeaponEntity = setWeaponEntity;
            _setArmorEntity = setArmorEntity;
            SetState(WeaponMenuUIState.SetWeaponPanel);
        }

        public void SetState(WeaponMenuUIState state)
            => _state = state;
    }
}