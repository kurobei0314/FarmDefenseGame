using UnityEngine;
using WolfVillage.Common;

namespace WolfVillage.Search.PlayerMenuUI
{
    public  class OwnedWeaponPanel : ScrollPanel<OwnedWeaponPanelVM>
    {
        [SerializeField] private GameObject _setIconGroup;
        [SerializeField] private WeaponPanel _weaponPanel;
        [SerializeField] private GameObject _selectedGroup;
        [SerializeField] private Animator _animator;

        public override void UpdateView()
        {   
            var vo = viewModel.weaponEntity.WeaponVO;
            _setIconGroup.SetActive(viewModel.isSet);
            _weaponPanel.Initialize(vo.Name, vo.RoleType, vo.AttackType);
        }
        public override void OnClick()
        {
            ClickAction.Invoke(viewModel);
        }

        public override void OnSelect()
        {
            _animator.SetBool("Select", true);
        }

        public override void OnUnSelect()
        {
            _animator.SetBool("Select", false);
        }
    }
}