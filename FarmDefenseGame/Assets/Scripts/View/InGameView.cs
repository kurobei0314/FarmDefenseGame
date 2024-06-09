using UnityEngine;

namespace WolfVillageBattle
{
    public class InGameView : MonoBehaviour
    {
        [SerializeField] private HPBarView _playerHPBarView = null;
        [SerializeField] private Transform _attackTextParent = null;

        public void Initialize(int playerMaxHP)
        {
            UpdatePlayerHPView(playerMaxHP);
            UpdateJustAvoidHPView(false);
        }

        public void UpdatePlayerHPView(int currentHP)
            => _playerHPBarView.SetValue(currentHP);

        public void UpdateJustAvoidHPView(bool active)
            => _attackTextParent.gameObject.SetActive(active);
    }
}
