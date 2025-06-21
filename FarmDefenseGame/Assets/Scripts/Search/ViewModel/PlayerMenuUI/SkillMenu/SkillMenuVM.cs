using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public enum FocusSkillMenuState
    {
        SetSkillIcon,
        OwnedSkillList,
    }

    public class SkillMenuVM
    {
        public FocusSkillMenuState State => _focusState;
        private FocusSkillMenuState _focusState;
        public int FocusSkillIndex => _focusSkillIndex;
        private int _focusSkillIndex;
        public RoleType CurrentFocusRoleType => _currentFocusRoleType;
        private RoleType _currentFocusRoleType;

        private int _maxIndex = GameInfo.PLAYER_SET_SKILL_NUM;

        public SkillMenuVM(RoleType setWeaponRoleType)
        {
            SetState(FocusSkillMenuState.SetSkillIcon);
            SetSkillIconIndex(0);
            SetCurrentFocusRoleType(setWeaponRoleType);
        }

        public void SetState(FocusSkillMenuState state)
            => _focusState = state;

        public void SetSkillIconIndex(int index)
            => _focusSkillIndex = index;

        public void SetCurrentFocusRoleType(RoleType type)
            => _currentFocusRoleType = type;
    }
}
