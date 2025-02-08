using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class EquipmentMenuUI : MonoBehaviour
    {
        [SerializeField] private OwnedEquipmentList _ownedEquipmentList;
        [SerializeField] private SetCurrentEquipmentPanel _currentEquipmentPanel;
        private EquipmentMenuVM _weaponMenuVM;
        private ISetEquipmentUseCase _equipmentUseCase;

        public void Initialize( PlayerInput playerInput,
                                ISetEquipmentUseCase equipmentUseCase)
        {
            _currentEquipmentPanel.Initialize(equipmentUseCase.PlayerCurrentWeapon, equipmentUseCase.PlayerCurrentArmor);
            _ownedEquipmentList.gameObject.SetActive(false);
            _weaponMenuVM = new EquipmentMenuVM(equipmentUseCase.PlayerCurrentWeapon, equipmentUseCase.PlayerCurrentArmor);
            _equipmentUseCase = equipmentUseCase;

            Observable.EveryUpdate()
                        .Where(_ => playerInput.actions[SearchGameInputActionName.StickInput].WasPressedThisFrame())
                        .Subscribe(_ => {
                            UpdateViewStickInput(playerInput);
                        }).AddTo(this);
        }

        // TODO: 絶対にUI全体を管理するクラスを作成し、そこで通知を受け取るようにする
        #region InputSystemEventHandler
        public void InputDecideEvent(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            UpdateViewDecide(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor, _equipmentUseCase.HasWeaponEntity, _equipmentUseCase.HasArmorEntity);
        }

        public void InputCancelEvent(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            UpdateViewCancel();
        }
        #endregion

        private void UpdateViewStickInput(PlayerInput playerInput)
        {
            var axis = playerInput.actions[SearchGameInputActionName.StickInput].ReadValue<Vector2>();
            switch (_weaponMenuVM.State)
            {
                case EquipmentMenuVM.FocusWeaponMenuUIState.SetWeaponPanel:
                case EquipmentMenuVM.FocusWeaponMenuUIState.SetArmorPanel:
                    if (axis.y > 0) 
                    {
                        _currentEquipmentPanel.FocusWeaponPanel();
                        _weaponMenuVM.SetState(EquipmentMenuVM.FocusWeaponMenuUIState.SetWeaponPanel);
                    }
                    if (axis.y < 0)
                    { 
                        _currentEquipmentPanel.FocusArmorPanel();
                        _weaponMenuVM.SetState(EquipmentMenuVM.FocusWeaponMenuUIState.SetArmorPanel);
                    }
                    break;
                case EquipmentMenuVM.FocusWeaponMenuUIState.OwnedWeaponList:
                case EquipmentMenuVM.FocusWeaponMenuUIState.OwnedArmorList:
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
                case EquipmentMenuVM.FocusWeaponMenuUIState.SetWeaponPanel:
                    UpdateViewForSetWeaponPanel(setWeaponEntity, ownedWeaponEntities);
                    break;
                case EquipmentMenuVM.FocusWeaponMenuUIState.SetArmorPanel:
                    UpdateViewForSetArmorPanel(setArmorEntity, ownedArmorEntities);
                    break;
                case EquipmentMenuVM.FocusWeaponMenuUIState.OwnedWeaponList:
                case EquipmentMenuVM.FocusWeaponMenuUIState.OwnedArmorList:
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
            _weaponMenuVM.SetState(EquipmentMenuVM.FocusWeaponMenuUIState.OwnedWeaponList);
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
            _weaponMenuVM.SetState(EquipmentMenuVM.FocusWeaponMenuUIState.OwnedArmorList);
        }

        private void UpdateViewCancel()
        {
            switch (_weaponMenuVM.State)
            {
                case EquipmentMenuVM.FocusWeaponMenuUIState.OwnedWeaponList:
                    _ownedEquipmentList.gameObject.SetActive(false);
                    _currentEquipmentPanel.SetEquipments(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor);
                    _weaponMenuVM.SetState(EquipmentMenuVM.FocusWeaponMenuUIState.SetWeaponPanel);
                    break;
                case EquipmentMenuVM.FocusWeaponMenuUIState.OwnedArmorList:
                    _ownedEquipmentList.gameObject.SetActive(false);
                    _currentEquipmentPanel.SetEquipments(_equipmentUseCase.PlayerCurrentWeapon, _equipmentUseCase.PlayerCurrentArmor);
                    _weaponMenuVM.SetState(EquipmentMenuVM.FocusWeaponMenuUIState.SetArmorPanel);
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
