using TMPro;
using UnityEngine;
using WolfVillage.Common;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class OwnedSkillListPanel : ScrollPanel<OwnedSkillListPanelVM>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _setIcon;

        public override void UpdateView()
        {   
            _name.text = viewModel.SkillEntity.SkillVO.Name;
            _setIcon.gameObject.SetActive(viewModel.IsSet);
        }
        public override void OnSelect()
        {
            SelectAction?.Invoke(viewModel);
        }

        public override void OnFocus()
        {
            _animator.SetBool("Select", true);
            FocusAction?.Invoke(viewModel);
        }

        public override void OnUnFocus()
        {
            _animator.SetBool("Select", false);
        }

        public override void Dispose()
        {
            _name = null;
        }
    }
}
