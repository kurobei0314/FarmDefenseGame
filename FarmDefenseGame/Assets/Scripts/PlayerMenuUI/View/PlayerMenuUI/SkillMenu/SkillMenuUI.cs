using System.Linq;
using UnityEngine;
using WolfVillage.Interface;
using Extension;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SkillMenuUI : PlayerMenuElementUI
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
        }

        protected override void ShowUI()
        {
            _skillMenuVM.SetState(FocusSkillMenuState.SetSkillIcon);
            UpdateView(_skillUseCase.SetWeaponRoleType);
            CloseOwnedSkillList();
            _skillDescription.Close();
        }

        private void UpdateView(RoleType type)
        {
            _skillRoleTypeToggleGroup.Initialize(type);
            _setCurrentSkillGroup.Initialize(type, _skillUseCase.GetCurrentSkillEntitiesByRoleType(type));
            _setCurrentSkillGroup.UpdateFocusView(_skillMenuVM.FocusSkillIndex, true);
            _ownedSkillList.gameObject.SetActive(_skillMenuVM.State == FocusSkillMenuState.OwnedSkillList);
        }

        protected override void InputStickEvent(Vector2 axis)
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
        protected override void InputDecideEvent()
        {
            switch (_skillMenuVM.State)
            {
                case FocusSkillMenuState.SetSkillIcon:
                    OpenOwnedSkillList();
                    _skillMenuVM.SetState(FocusSkillMenuState.OwnedSkillList);
                    break;
                case FocusSkillMenuState.OwnedSkillList:
                    _ownedSkillList.SelectFocusIndex();
                    CloseOwnedSkillList();
                    _skillMenuVM.SetState(FocusSkillMenuState.SetSkillIcon);
                    break;
            }
        }

        protected override void InputCancelEvent()
        {
        }

        protected override void InputSwitchSubCategoryEvent(int index)
        {
            var nextIndex = (int)_skillMenuVM.CurrentFocusRoleType + index;
            if      (nextIndex < EnumHelper.GetMinIndexEnum<RoleType>()) nextIndex = EnumHelper.GetMaxIndexEnum<RoleType>();
            else if (EnumHelper.GetMaxIndexEnum<RoleType>() < nextIndex) nextIndex = EnumHelper.GetMinIndexEnum<RoleType>();

            _skillRoleTypeToggleGroup.SwitchTab((RoleType)nextIndex, () =>
            {
                _skillMenuVM.SetCurrentFocusRoleType((RoleType)nextIndex);
                UpdateView((RoleType)nextIndex);
            });
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

        private void OpenOwnedSkillList()
        {
            _ownedSkillList.Open();
            _skillDescription.Open();
            UpdateOwnedSkillListView(_skillMenuVM.CurrentFocusRoleType);
        }

        private void CloseOwnedSkillList()
        {
            _skillDescription.Close();
            _ownedSkillList.Close();
        }

        private void UpdateOwnedSkillListView(RoleType type)
        {
            var vms = _skillUseCase.GetHasSkillEntitiesByRoleType(type)
                                    .Select(skill => new OwnedSkillListPanelVM(skill.Id, skill, IsSetSkill(type, skill.Id)))
                                    .Where(skill => skill.SkillEntity.SkillVO.RoleType == _skillMenuVM.CurrentFocusRoleType)
                                    .ToArray();
            _ownedSkillList.Initialize( vms, 
                                        (skill) => _skillDescription.SetText(skill.SkillEntity.SkillVO.Description),
                                        (skill) => 
                                        { 
                                            _skillUseCase.UpdateCurrentSkill(skill.SkillEntity, _skillMenuVM.FocusSkillIndex);
                                            UpdateView(_skillMenuVM.CurrentFocusRoleType);
                                        });
        }

        private bool IsSetSkill(RoleType type, int skillId)
        {
            var setSkills = _skillUseCase.GetCurrentSkillEntitiesByRoleType(type);
            for (var i = 0; i < setSkills.Length; i++)
            {
                if (setSkills[i] == null) continue;
                if (setSkills[i].Id == skillId) return true;
            }
            return false;
        }

        public override void Dispose()
        {
            _ownedSkillList.Dispose();
            _setCurrentSkillGroup.Dispose();
        }
    }
}
