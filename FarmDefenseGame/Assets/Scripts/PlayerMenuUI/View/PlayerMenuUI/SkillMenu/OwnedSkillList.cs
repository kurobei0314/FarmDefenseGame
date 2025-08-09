using WolfVillage.Common;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class OwnedSkillList : VerticalScrollList<OwnedSkillListPanel, OwnedSkillListPanelVM>
    {
        public void Open()
            => this.gameObject.SetActive(true);

        public void Close()
            => this.gameObject.SetActive(false);
    }
}
