using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class HasWeaponPanel : MonoBehaviour
    {

        [SerializeField] private WeaponPanel _weaponPanel;
        [SerializeField] private GameObject _selectedGroup;

        public void Initialize(string weaponName, RoleType roleType, AttackType attackType)
        {
            _weaponPanel.Initialize( weaponName, roleType, attackType);
        }

        public void SetSelectedPanel()
            => _selectedGroup.SetActive(true);

        public void SetUnSelectedPanel()
            => _selectedGroup.SetActive(false);
    }
}
