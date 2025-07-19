namespace WolfVillage.Search.PlayerMenuUI
{
    public enum PlayerMenuState
    {
        Status = 0,
        Equipment = 1,
        Skill = 2,
        Inventory = 3,
        Setting = 4,
    }

    public class PlayerMenuUIVM
    {
        private PlayerMenuState m_state;
        public PlayerMenuUIVM()
        {
            // TODO: 後でStatusを最初にする
            m_state = PlayerMenuState.Skill;
        }

        public void SetPlayerMenuState(PlayerMenuState state)
            => m_state = state;
        public PlayerMenuState State => m_state;
    }
}