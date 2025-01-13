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
            _ownedWeaponList.Initialize(setWeaponEntity, ownedWeaponEntities);
            _ownedWeaponList.gameObject.SetActive(false);
            _weaponMenuVM = new WeaponMenuVM(setWeaponEntity, setArmorEntity);

            Observable.EveryUpdate()
                        .Where(_ => playerInput.actions[SearchGameInputActionName.StickInput].WasPressedThisFrame())
                        .Subscribe(_ => {
                            var axis = playerInput.actions[SearchGameInputActionName.StickInput].ReadValue<Vector2>();
                            if (axis.y > 0) _currentWeaponPanel.SelectWeaponPanel();
                            if (axis.y < 0) _currentWeaponPanel.SelectArmorPanel();
                        }).AddTo(this);

            Observable.EveryUpdate()
                        .Where(_ => playerInput.actions[SearchGameInputActionName.Decide].IsPressed())
                        .Subscribe(_ => {
                            if (_weaponMenuVM.State == WeaponMenuVM.WeaponMenuUIState.SetWeaponPanel)
                            {
                                _ownedWeaponList.gameObject.SetActive(true);
                                _weaponMenuVM.SetState(WeaponMenuVM.WeaponMenuUIState.OwnedWeaponList);
                            }
                        });
        }

        public void Dispose()
        {

        }
    }
}
