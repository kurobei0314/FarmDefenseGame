using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IInGameView
    {
        public void UpdatePlayerHPView(float currentHP);
        public void UpdateJustAvoidViewActive(bool active);
    }
}
