using System.Linq;
using R3;
using UnityEngine;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI.EquipmentMenu
{
    public class EquipmentMenuUI : PlayerMenuElementUI
    {
        [SerializeField] private OwnedEquipmentList _ownedEquipmentList;
        [SerializeField] private SetCurrentEquipmentPanel _currentEquipmentPanel;
        private EquipmentMenuVM _weaponMenuVM;
        private ISetEquipmentUseCase _equipmentUseCase;

        public void Initialize(ISetEquipmentUseCase equipmentUseCase)
        {
            _currentEquipmentPanel.Initialize(equipmentUseCase.PlayerCurrentWeapon, equipmentUseCase.PlayerCurrentArmor);
            _ownedEquipmentList.gameObject.SetActive(false);
            _weaponMenuVM = new EquipmentMenuVM(equipmentUseCase.PlayerCurrentWeapon, equipmentUseCase.PlayerCurrentArmor);
            _equipmentUseCase = equipmentUseCase;
        }
        protected override void InputStickEvent(Vector2 axis)
        {
            switch (_weaponMenuVM.State)
            {
                case FocusEquipmentMenuState.SetWeaponPanel:
                case FocusEquipmentMenuState.SetArmorPanel:
                    if (axis.y > 0) 
                    {
                        _currentEquipmentPanel.FocusWeaponPanel();
                        _weaponMenuVM.SetState(FocusEquipmentMenuState.SetWeaponPanel);
                    }
                    if (axis.y < 0)
                    { 
                        _currentEquipmentPanel.FocusArmorPanel();
                        _weaponMenuVM.SetState(FocusEquipmentMenuState.SetArmorPanel);
                    }
                    break;
                case FocusEquipmentMenuState.OwnedWeaponList:
                case FocusEquipmentMenuState.OwnedArmorList:
                    _ownedEquipmentList.UpdateFocusIndex(axis);
                    break;
            }
        }
        protected override void InputDecideEvent()
        {
            var setWeaponEntity = _equipmentUseCase.PlayerCurrentWeapon;
            var setArmorEntity = _equipmentUseCase.PlayerCurrentArmor;
            var ownedWeaponEntities = _equipmentUseCase.HasWeaponEntity;
            var ownedArmorEntities = _equipmentUseCase.HasArmorEntity;
            switch (_weaponMenuVM.State)
            {
                case FocusEquipmentMenuState.SetWeaponPanel:
                    UpdateViewForSetWeaponPanel(setWeaponEntity, ownedWeaponEntities);
                    break;
                case FocusEquipmentMenuState.SetArmorPanel:
                    UpdateViewForSetArmorPanel(setArmorEntity, ownedArmorEntities);
                    break;
                case FocusEquipmentMenuState.OwnedWeaponList:
                case FocusEquipmentMenuState.OwnedArmorList:
                    _ownedEquipmentList.SelectFocusIndex();
                    break;
            }
        }

        protected override void InputCancelEvent()
        {
            switch (_weaponMenuVM.State)
            {
                case FocusEquipmentMenuState.OwnedWeaponList:
                    _ownedEquipmentList.gameObject.SetActive(false);
                    _currentEquipmentPanel.SetEquipments(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor);
                    _weaponMenuVM.SetState(FocusEquipmentMenuState.SetWeaponPanel);
                    break;
                case FocusEquipmentMenuState.OwnedArmorList:
                    _ownedEquipmentList.gameObject.SetActive(false);
                    _currentEquipmentPanel.SetEquipments(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor);
                    _weaponMenuVM.SetState(FocusEquipmentMenuState.SetArmorPanel);
                    break;
            }
        }

        protected override void InputSwitchSubCategoryEvent(int index)
        {
            return;
        }

        private void UpdateViewForSetWeaponPanel(IWeaponEntity setWeaponEntity, IWeaponEntity[] ownedWeaponEntities)
        {
            _ownedEquipmentList.gameObject.SetActive(true);
            var panelVMs = ownedWeaponEntities.Select(entity => new OwnedEquipmentPanelVM(entity.Id, entity.WeaponVO.Name, entity.WeaponVO.Attack, 0, entity.Id == setWeaponEntity.Id)).ToArray();
            _ownedEquipmentList.Initialize(panelVMs, (vm) => _currentEquipmentPanel.SetEquipmentChangeStatus(vm.StatusPanelVM),
            (vm) =>
            {
                _equipmentUseCase.SetCurrentWeapon(vm.Id);
                _currentEquipmentPanel.SetEquipments(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor);
                InputCancelEvent();
            });
            _weaponMenuVM.SetState(FocusEquipmentMenuState.OwnedWeaponList);
        }

        private void UpdateViewForSetArmorPanel(IArmorEntity setArmorEntity, IArmorEntity[] ownedArmorEntities)
        {
            _ownedEquipmentList.gameObject.SetActive(true);
            var panelVMs = ownedArmorEntities.Select(entity => new OwnedEquipmentPanelVM(entity.Id, entity.ArmorVO.Name, 0, entity.ArmorVO.Defense, entity.Id == setArmorEntity.Id)).ToArray();
            _ownedEquipmentList.Initialize(panelVMs, (vm) => _currentEquipmentPanel.SetEquipmentChangeStatus(vm.StatusPanelVM),
            (vm) => 
            {
                _equipmentUseCase.SetCurrentArmor(vm.Id);
                _currentEquipmentPanel.SetEquipments(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor);
                InputCancelEvent();
            });
            _weaponMenuVM.SetState(FocusEquipmentMenuState.OwnedArmorList);
        }

        public override void Dispose()
        {
            _ownedEquipmentList.Dispose();
            _currentEquipmentPanel.Dispose();
        }
    }
}
