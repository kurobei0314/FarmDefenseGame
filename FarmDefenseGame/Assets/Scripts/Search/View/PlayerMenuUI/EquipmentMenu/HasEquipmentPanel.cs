using UnityEngine;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class HasEquipmentPanel : MonoBehaviour
    {
        [SerializeField] private EquipmentPanel _equipmentPanel;
        [SerializeField] private Animator _animator;

        public void Initialize(string name)
        {
            _equipmentPanel.Initialize(name);
        }

        public void SetSelectedPanel()
            => _animator.SetBool("Select", true);

        public void SetUnSelectedPanel()
            => _animator.SetBool("Select", false);

        public void Dispose()
        {
            _equipmentPanel.Dispose();
        }
    }
}
