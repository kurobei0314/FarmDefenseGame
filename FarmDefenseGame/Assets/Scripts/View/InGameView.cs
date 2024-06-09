using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class InGameView : MonoBehaviour, IInGameView
    {
        [SerializeField] private HPBarView _playerHPBarView = null;
        [SerializeField] private Transform _attackTextParent = null;

        public void Initialize(float playerMaxHP)
        {
            UpdatePlayerHPView(playerMaxHP);
            UpdateJustAvoidViewActive(false);
        }

        public void UpdatePlayerHPView(float currentHP)
            => _playerHPBarView.SetValue(currentHP);

        public void UpdateJustAvoidViewActive(bool active)
            => _attackTextParent.gameObject.SetActive(active);
    }
}
