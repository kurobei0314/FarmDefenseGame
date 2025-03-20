namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public enum FocusSkillMenuState
    {
        SetSkillIcon,
        OwnedSkillList,
    }

    public class SkillMenuVM
    {
        private FocusSkillMenuState _focusState;
        public SkillMenuVM()
        {
            SetState(FocusSkillMenuState.SetSkillIcon);
        }

        public void SetState(FocusSkillMenuState state)
            => _focusState = state;
    }
}
