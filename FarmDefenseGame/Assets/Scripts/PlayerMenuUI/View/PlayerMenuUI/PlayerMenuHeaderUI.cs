using UnityEngine;
using System.Collections.Generic;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class PlayerMenuHeaderUI : MonoBehaviour
    {
        [SerializeField] private List<PlayerMenuHeaderIconUI> _icons;
        private int _iconIndex;

        public void UpdateView(PlayerMenuState currentPlayerMenuState)
        {
            _icons[_iconIndex].SetOff();
            _iconIndex = (int)currentPlayerMenuState;
            _icons[_iconIndex].SetOn();
        }

        public void Dispose()
        {
            for(var i = 0; i < _icons.Count; i++)
            {
                _icons[i].Dispose();
            }
        }
    }
}
