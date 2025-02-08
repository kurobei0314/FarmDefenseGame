using TMPro;
using UnityEngine;

namespace  WolfVillage.Search.PlayerMenuUI
{
    public class PlayerStatusChangerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentAttackText;
        [SerializeField] private TMP_Text _currentDefenseText;
        [SerializeField] private GameObject _changerAttackParent;
        [SerializeField] private GameObject _changerDefenseParent;
        [SerializeField] private TMP_Text _changerAttackText;
        [SerializeField] private TMP_Text _changerDefenseText;

        [SerializeField] private Color _upStatusColor;
        [SerializeField] private Color _downStatusColor;
        [SerializeField] private Color _noChangeStatusColor;

        private int _currentAttack;
        private int _currentDefense;

        public void Initialize(int currentAttack, int currentDefense)
        {
            _currentAttack = currentAttack;
            _currentDefense = currentDefense;
            _currentAttackText.text = currentAttack.ToString();
            _currentDefenseText.text = currentDefense.ToString();
            _changerAttackParent.SetActive(false);
            _changerDefenseParent.SetActive(false);
        }

        public void SetEquipmentChangeStatus(SetEquipmentChangeStatusVM statusVM)
        {
            SetChangeAttack(statusVM.AddAttack);
            SetChangeDefense(statusVM.AddDefense);
        }

        private void SetChangeAttack(int changerAttack)
        {
            _changerAttackParent.SetActive(changerAttack != 0);
            if (changerAttack == 0) return;
            _changerAttackText.text = changerAttack.ToString();
            _changerAttackText.color = GetTextColor(changerAttack - _currentAttack);
        }

        private void SetChangeDefense(int changerDefense)
        {
            _changerDefenseParent.SetActive(changerDefense != 0);
            if (changerDefense == 0) return;
            _changerDefenseText.text = changerDefense.ToString();
            _changerDefenseText.color = GetTextColor(changerDefense - _currentDefense);
        }

        private Color GetTextColor(int statusDifference)
        {
            if (statusDifference > 0) return _upStatusColor;
            if (statusDifference < 0) return _downStatusColor;
            return _noChangeStatusColor;
        }
    }
}
