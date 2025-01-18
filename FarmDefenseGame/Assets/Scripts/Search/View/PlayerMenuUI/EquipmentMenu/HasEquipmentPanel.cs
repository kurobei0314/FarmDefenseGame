using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class HasEquipmentPanel : MonoBehaviour
    {
        [SerializeField] private EquipmentPanel _weaponPanel;
        [SerializeField] private Animator _animator;

        public void Initialize(string weaponName, RoleType roleType, AttackType attackType)
        {
            _weaponPanel.Initialize( weaponName, roleType, attackType);
        }

        public void SetSelectedPanel()
            => _animator.SetBool("Select", true);

        public void SetUnSelectedPanel()
            => _animator.SetBool("Select", false);
    }
}
