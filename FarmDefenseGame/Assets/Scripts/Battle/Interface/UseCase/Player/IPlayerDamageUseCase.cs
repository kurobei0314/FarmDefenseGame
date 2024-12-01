using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerDamageUseCase 
    {
        void HitEnemyAttack(Collision enemy);
        void Damage();
        void Die();
    }
}

