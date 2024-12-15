using UnityEngine;
using WolfVillage.Common;

namespace WolfVillage.Search.PlayerMenuUI
{
    public  class OwnedWeaponPanel : ScrollPanel<OwnedWeaponPanelVM>
    {
        [SerializeField] private GameObject _setIconGroup;
        [SerializeField] private HasWeaponPanel _weaponPanel;
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
    }
}
