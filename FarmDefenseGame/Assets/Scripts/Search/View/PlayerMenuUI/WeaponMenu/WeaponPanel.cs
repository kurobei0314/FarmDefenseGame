using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class WeaponPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _weaponName;
        [SerializeField] private Image _weaponIcon;

        public void Initialize(string weaponName, RoleType roleType, AttackType attackType)
        {
            _weaponName.text = weaponName;
            
        }
    }
}
