using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI.EquipmentMenu
{
    public class EquipmentMenuUI : MonoBehaviour, IPlayerMenuUIInputter
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
        void IPlayerMenuUIInputter.InputStickEvent(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            var value = context.ReadValue<Vector2>();
            UpdateViewStickInput(value);
        }
        void IPlayerMenuUIInputter.InputDecideEvent(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            UpdateViewDecide(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor, _equipmentUseCase.HasWeaponEntity, _equipmentUseCase.HasArmorEntity);
        }

        void IPlayerMenuUIInputter.InputCancelEvent(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            UpdateViewCancel();
        }

        void IPlayerMenuUIInputter.SetActive(bool active)
            => this.gameObject.SetActive(active);

        private void UpdateViewStickInput(Vector2 axis)
        {
            switch (_weaponMenuVM.State)
            {
                case FocusWeaponMenuUIState.SetWeaponPanel:
                case FocusWeaponMenuUIState.SetArmorPanel:
                    if (axis.y > 0) 
                    {
                        _currentEquipmentPanel.FocusWeaponPanel();
                        _weaponMenuVM.SetState(FocusWeaponMenuUIState.SetWeaponPanel);
                    }
                    if (axis.y < 0)
                    { 
                        _currentEquipmentPanel.FocusArmorPanel();
                        _weaponMenuVM.SetState(FocusWeaponMenuUIState.SetArmorPanel);
                    }
                    break;
                case FocusWeaponMenuUIState.OwnedWeaponList:
                case FocusWeaponMenuUIState.OwnedArmorList:
                    _ownedEquipmentList.UpdateFocusIndex(axis);
                    break;
            }
        }

        private void UpdateViewDecide(IWeaponEntity setWeaponEntity,
                                      IArmorEntity setArmorEntity, 
                                      IWeaponEntity[] ownedWeaponEntities,
                                      IArmorEntity[] ownedArmorEntities)
        {
            switch (_weaponMenuVM.State)
            {
                case FocusWeaponMenuUIState.SetWeaponPanel:
                    UpdateViewForSetWeaponPanel(setWeaponEntity, ownedWeaponEntities);
                    break;
                case FocusWeaponMenuUIState.SetArmorPanel:
                    UpdateViewForSetArmorPanel(setArmorEntity, ownedArmorEntities);
                    break;
                case FocusWeaponMenuUIState.OwnedWeaponList:
                case FocusWeaponMenuUIState.OwnedArmorList:
                    _ownedEquipmentList.SelectFocusIndex();
                    break;
            }
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
                UpdateViewCancel();
            });
            _weaponMenuVM.SetState(FocusWeaponMenuUIState.OwnedWeaponList);
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
                UpdateViewCancel();
            });
            _weaponMenuVM.SetState(FocusWeaponMenuUIState.OwnedArmorList);
        }

        private void UpdateViewCancel()
        {
            switch (_weaponMenuVM.State)
            {
                case FocusWeaponMenuUIState.OwnedWeaponList:
                    _ownedEquipmentList.gameObject.SetActive(false);
                    _currentEquipmentPanel.SetEquipments(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor);
                    _weaponMenuVM.SetState(FocusWeaponMenuUIState.SetWeaponPanel);
                    break;
                case FocusWeaponMenuUIState.OwnedArmorList:
                    _ownedEquipmentList.gameObject.SetActive(false);
                    _currentEquipmentPanel.SetEquipments(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor);
                    _weaponMenuVM.SetState(FocusWeaponMenuUIState.SetArmorPanel);
                    break;
            }
        }

        public void Dispose()
        {
            _ownedEquipmentList.Dispose();
            _currentEquipmentPanel.Dispose();
        }
    }
}
