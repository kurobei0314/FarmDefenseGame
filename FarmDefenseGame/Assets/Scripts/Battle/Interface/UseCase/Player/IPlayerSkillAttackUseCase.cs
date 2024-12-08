using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillage.Battle.Interface
{
    public interface IPlayerSkillAttackUseCase
    {
        public void AttackPlayer(int index);
        public void FinishIntervalTimeSkill(int index);
    }
}
