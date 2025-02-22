namespace WolfVillage.Search.PlayerMenuUI.EquipmentMenu
{
    public class SetEquipmentChangeStatusVM
    {
        private int m_addAttack;
        private int m_addDefense;

        public SetEquipmentChangeStatusVM(int addAttack, int addDefense)
        {
            m_addAttack = addAttack;
            m_addDefense = addDefense;
        }
        public int AddAttack => m_addAttack;
        public int AddDefense => m_addDefense;
    }
}
