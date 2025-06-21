using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SkillMenuUI : MonoBehaviour, IPlayerMenuUIInputter
    {
        [SerializeField] private SkillRoleTypeToggleGroup _skillRoleTypeToggleGroup;
        [SerializeField] private SetCurrentSkillGroup _setCurrentSkillGroup;
        [SerializeField] private OwnedSkillList _ownedSkillList;
        [SerializeField] private SkillDescription _skillDescription;
        private ISetSkillUseCase _skillUseCase;
        private SkillMenuVM _skillMenuVM;

        public void Initialize(ISetSkillUseCase skillUseCase)
        {
            _skillMenuVM = new SkillMenuVM(skillUseCase.SetWeaponRoleType);
            _skillUseCase = skillUseCase;
            UpdateView(skillUseCase.SetWeaponRoleType);
            CloseOwnedSkillList();
        }

        private void UpdateView(RoleType type)
        {
            _skillRoleTypeToggleGroup.Initialize(type);
            _setCurrentSkillGroup.Initialize(type, _skillUseCase.GetCurrentSkillEntitiesByRoleType(type));
            _setCurrentSkillGroup.UpdateFocusView(_skillMenuVM.FocusSkillIndex, true);
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
            UpdateViewDecideInput();
        }

        void IPlayerMenuUIInputter.InputCancelEvent(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
        }

        void IPlayerMenuUIInputter.SetActive(bool active)
            => this.gameObject.SetActive(active);

        private void UpdateViewStickInput(Vector2 axis)
        {
            switch (_skillMenuVM.State)
            {
                case FocusSkillMenuState.SetSkillIcon:
                    UpdateFocusSkillIndexView(axis);
                    break;
                case FocusSkillMenuState.OwnedSkillList:
                    _ownedSkillList.UpdateFocusIndex(axis);
                    break;
            }
        }

        private void UpdateFocusSkillIndexView(Vector2 axis)
        {
            var addIndex = axis.y > 0 ? -1 : 1;
            if (axis.y == 0) addIndex = axis.x > 0 ? 1 : -1;
            var nextIndex = _skillMenuVM.FocusSkillIndex + addIndex;

            if (nextIndex < 0 || GameInfo.PLAYER_SET_SKILL_NUM <= nextIndex) return;
            _setCurrentSkillGroup.UpdateFocusView(_skillMenuVM.FocusSkillIndex, false);
            _setCurrentSkillGroup.UpdateFocusView(nextIndex, true);
            _skillMenuVM.SetSkillIconIndex(nextIndex);
        }

        private void UpdateViewDecideInput()
        {
            switch (_skillMenuVM.State)
            {
                case FocusSkillMenuState.SetSkillIcon:
                    OpenOwnedSkillList();
                    break;
                case FocusSkillMenuState.OwnedSkillList:
                    break;
            }
        }

        private void OpenOwnedSkillList()
        {
            _ownedSkillList.gameObject.SetActive(true);
            UpdateOwnedSkillListView(_skillMenuVM.CurrentFocusRoleType);
        }

        private void CloseOwnedSkillList()
            => _ownedSkillList.gameObject.SetActive(false);

        private void UpdateOwnedSkillListView(RoleType type)
        {
            var vms = _skillUseCase.GetHasSkillEntitiesByRoleType(type)
                                   .Select(skill => new OwnedSkillListPanelVM(skill.Id, skill)).ToArray();
            _ownedSkillList.Initialize(vms, null, null);
        }

        public void Dispose()
        {
            _ownedSkillList.Dispose();
            _setCurrentSkillGroup.Dispose();
        }
    }
}
