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

        private int _maxIndex = GameInfo.PLAYER_SET_SKILL_NUM;

        public SkillMenuVM()
        {
            SetState(FocusSkillMenuState.SetSkillIcon);
            SetSkillIconIndex(0);
        }

        public void SetState(FocusSkillMenuState state)
            => _focusState = state;

        public void SetSkillIconIndex(int index)
            => _focusSkillIndex = index;
    }
}
