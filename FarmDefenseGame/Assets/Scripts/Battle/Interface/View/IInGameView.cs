using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillage.Battle.Interface
{
    public interface IInGameView
    {
        PlayerSkillIconView[] PlayerSkillIconViews { get; }
        public void UpdatePlayerHPView(float currentHP);
        public void UpdateJustAvoidViewActive(bool active);
        public void UpdateSkillIconViewForUseSkill(int index);
    }
}
