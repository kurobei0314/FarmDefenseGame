using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class WeaponMenuUI : MonoBehaviour
    {
        [SerializeField] private OwnedWeaponList _ownedWeaponList;
        [SerializeField] private SetCurrentWeaponPanel _currentWeaponPanel;
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
                        });
        }
        private void UpdateViewStickInput(PlayerInput playerInput)
        {
            var axis = playerInput.actions[SearchGameInputActionName.StickInput].ReadValue<Vector2>();
            switch (_weaponMenuVM.State)
            {
                case WeaponMenuVM.WeaponMenuUIState.SetWeaponPanel:
                case WeaponMenuVM.WeaponMenuUIState.SetArmorPanel:
                    if (axis.y > 0) _currentWeaponPanel.SelectWeaponPanel();
                    if (axis.y < 0) _currentWeaponPanel.SelectArmorPanel();
                    break;
                case WeaponMenuVM.WeaponMenuUIState.OwnedWeaponList:
                case WeaponMenuVM.WeaponMenuUIState.OwnedArmorList:
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
                case WeaponMenuVM.WeaponMenuUIState.SetWeaponPanel:
                    _ownedWeaponList.gameObject.SetActive(true);
                    _ownedWeaponList.Initialize(setWeaponEntity, ownedWeaponEntities);
                    _weaponMenuVM.SetState(WeaponMenuVM.WeaponMenuUIState.OwnedWeaponList);
                    break;
            }
        }

        public void Dispose()
        {

        }
    }
}
