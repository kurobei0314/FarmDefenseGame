using UnityEngine;
using WolfVillage.Common;

namespace WolfVillage.Search.PlayerMenuUI
{
    public  class OwnedEquipmentPanel : ScrollPanel<OwnedEquipmentPanelVM>
    {
        [SerializeField] private GameObject _setIconGroup;
        [SerializeField] private EquipmentPanel _weaponPanel;
        [SerializeField] private GameObject _selectedGroup;
        [SerializeField] private Animator _animator;

        public override void UpdateView()
        {   
            var vo = viewModel.weaponEntity.WeaponVO;
            _setIconGroup.SetActive(viewModel.isSet);
            _weaponPanel.Initialize(vo.Name, vo.RoleType, vo.AttackType);
        }
        public override void OnSelect()
        {
            SelectAction.Invoke(viewModel);
        }

        public override void OnFocus()
        {
            _animator.SetBool("Select", true);
        }

        public override void OnUnFocus()
        {
            _animator.SetBool("Select", false);
        }
    }
}
