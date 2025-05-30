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
            _skillMenuVM = new SkillMenuVM();
            _skillUseCase = skillUseCase;
            UpdateView(skillUseCase.SetWeaponRoleType);
        }

        private void UpdateView(RoleType type)
        {
            _skillRoleTypeToggleGroup.Initialize(type);
            _setCurrentSkillGroup.Initialize(type, _skillUseCase.GetCurrentSkillEntitiesByRoleType(type));
            var vms = _skillUseCase.GetHasSkillEntitiesByRoleType(type)
                                   .Select(skill => new OwnedSkillListPanelVM(skill.Id, skill)).ToArray();
            _ownedSkillList.Initialize(vms, null, null);
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

                    break;
                case FocusSkillMenuState.OwnedSkillList:
                    _ownedSkillList.UpdateFocusIndex(axis);
                    break;
            }
        }

        private void UpdateFocusSkillIndexView(Vector2 axis)
        {
            
        }

        public void Dispose()
        {
            _ownedSkillList.Dispose();
        }
    }
}
