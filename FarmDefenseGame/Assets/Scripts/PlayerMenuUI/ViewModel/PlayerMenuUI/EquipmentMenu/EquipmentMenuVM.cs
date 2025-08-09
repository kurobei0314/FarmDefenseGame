using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI.EquipmentMenu
{
    public enum FocusEquipmentMenuState
    {
        SetWeaponPanel,
        SetArmorPanel,
        OwnedWeaponList,
        OwnedArmorList,
    }

    public class EquipmentMenuVM 
    {
        private FocusEquipmentMenuState _state;
        public FocusEquipmentMenuState State => _state;

        private IWeaponEntity _setWeaponEntity;
        private IArmorEntity _setArmorEntity;

        public EquipmentMenuVM(IWeaponEntity setWeaponEntity, IArmorEntity setArmorEntity)
        {
            _setWeaponEntity = setWeaponEntity;
            _setArmorEntity = setArmorEntity;
            SetState(FocusEquipmentMenuState.SetWeaponPanel);
        }

        public void SetState(FocusEquipmentMenuState state)
            => _state = state;
    }
}