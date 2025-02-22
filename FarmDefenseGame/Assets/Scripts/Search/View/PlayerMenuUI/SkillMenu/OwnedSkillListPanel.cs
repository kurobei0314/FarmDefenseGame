using UnityEngine;
using WolfVillage.Common;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class OwnedSkillListPanel : ScrollPanel<OwnedSkillListPanelVM>
    {
        [SerializeField] private Animator _animator;
        public override void UpdateView()
        {   
            
        }
        public override void OnSelect()
        {
        
        }

        public override void OnFocus()
        {
            _animator.SetBool("Select", true);
        }

        public override void OnUnFocus()
        {
            _animator.SetBool("Select", false);
        }

        public override void Dispose()
        {
            
        }
    }
}
