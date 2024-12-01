using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IEnemyDamageUseCase 
    {
        void ReduceHP(IWeaponEntity setWeapon);
        void Damage();
        void Die();
    }
}

