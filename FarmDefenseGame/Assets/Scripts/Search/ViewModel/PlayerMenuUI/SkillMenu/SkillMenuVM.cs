namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public enum FocusSkillMenuState
    {
        SetSkillIcon1,
        SetSkillIcon2,
        SetSkillIcon3,
        OwnedSkillList,
    }

    public class SkillMenuVM
    {
        private FocusSkillMenuState _focusState;
        public SkillMenuVM()
        {
            SetState(FocusSkillMenuState.SetSkillIcon1);
        }

        public void SetState(FocusSkillMenuState state)
            => _focusState = state;
    }
}
