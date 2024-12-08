using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillage.Battle.Interface
{
    public interface IEnemyAttackUseCase
    {
        void StartAttack();
        void StopAttack();
    }
}
