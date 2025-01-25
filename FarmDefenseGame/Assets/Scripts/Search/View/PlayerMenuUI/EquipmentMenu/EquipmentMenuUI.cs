using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class EquipmentMenuUI : MonoBehaviour
    {
        [SerializeField] private OwnedEquipmentList _ownedWeaponList;
        [SerializeField] private SetCurrentEquipmentPanel _currentWeaponPanel;
        private WeaponMenuVM _weaponMenuVM;

        public void Initialize( IWeaponEntity setWeaponEntity,
                                IArmorEntity setArmorEntity, 
                                IWeaponEntity[] ownedWeaponEntities,
                                IArmorEntity[] ownedArmorEntities,
                                PlayerInput playerInput)
        {
            _currentWeaponPanel.Initialize(setWeaponEntity, setArmorEntity);
            _ownedWeaponList.gameObject.SetActive(false);
            _weaponMenuVM = new WeaponMenuVM(setWeaponEntity, setArmorEntity);

            Observable.EveryUpdate()
                        .Where(_ => playerInput.actions[SearchGameInputActionName.StickInput].WasPressedThisFrame())
                        .Subscribe(_ => {
                            UpdateViewStickInput(playerInput);
                        }).AddTo(this);

            Observable.EveryUpdate()
                        .Where(_ => playerInput.actions[SearchGameInputActionName.Decide].IsPressed())
                        .Subscribe(_ => {
                            UpdateViewDecide(setWeaponEntity, setArmorEntity, ownedWeaponEntities, ownedArmorEntities);
                        }).AddTo(this);
        }
        private void UpdateViewStickInput(PlayerInput playerInput)
        {
            var axis = playerInput.actions[SearchGameInputActionName.StickInput].ReadValue<Vector2>();
            switch (_weaponMenuVM.State)
            {
                case WeaponMenuVM.FocusWeaponMenuUIState.SetWeaponPanel:
                case WeaponMenuVM.FocusWeaponMenuUIState.SetArmorPanel:
                    if (axis.y > 0) 
                    {
                        _currentWeaponPanel.SelectWeaponPanel();
                        _weaponMenuVM.SetState(WeaponMenuVM.FocusWeaponMenuUIState.SetWeaponPanel);
                    }
                    if (axis.y < 0)
                    { 
                        _currentWeaponPanel.SelectArmorPanel();
                        _weaponMenuVM.SetState(WeaponMenuVM.FocusWeaponMenuUIState.SetArmorPanel);
                    }
                    break;
                case WeaponMenuVM.FocusWeaponMenuUIState.OwnedWeaponList:
                case WeaponMenuVM.FocusWeaponMenuUIState.OwnedArmorList:
                    _ownedWeaponList.UpdateFocusIndex(axis);
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
                case WeaponMenuVM.FocusWeaponMenuUIState.SetWeaponPanel:
                    UpdateViewForSetWeaponPanel(setWeaponEntity, ownedWeaponEntities);
                    break;
                case WeaponMenuVM.FocusWeaponMenuUIState.SetArmorPanel:
                    UpdateViewForSetWeaponPanel(setArmorEntity, ownedArmorEntities);
                    break;
            }
        }
        private void UpdateViewForSetWeaponPanel(IWeaponEntity setWeaponEntity, IWeaponEntity[] ownedWeaponEntities)
        {
            _ownedWeaponList.gameObject.SetActive(true);
            var panelVMs = ownedWeaponEntities.Select(entity => new OwnedEquipmentPanelVM(entity.Id, entity.WeaponVO.Name, entity.Id == setWeaponEntity.Id)).ToArray();
            _ownedWeaponList.Initialize(panelVMs, null);
            _weaponMenuVM.SetState(WeaponMenuVM.FocusWeaponMenuUIState.OwnedWeaponList);
        }

        private void UpdateViewForSetWeaponPanel(IArmorEntity setArmorEntity, IArmorEntity[] ownedArmorEntities)
        {
            _ownedWeaponList.gameObject.SetActive(true);
            var panelVMs = ownedArmorEntities.Select(entity => new OwnedEquipmentPanelVM(entity.Id, entity.ArmorVO.Name, entity.Id == setArmorEntity.Id)).ToArray();
            _ownedWeaponList.Initialize(panelVMs, null);
            _weaponMenuVM.SetState(WeaponMenuVM.FocusWeaponMenuUIState.OwnedArmorList);
        }

        public void Dispose()
        {
            _ownedWeaponList.Dispose();
            _currentWeaponPanel.Dispose();
        }
    }
}
