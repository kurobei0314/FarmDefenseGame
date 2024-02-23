using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyDamageActor : IEnemyDamageUseCase
    {
        private IEnemyView enemyView;
        private IEnemyEntity enemyEntity;

        public EnemyDamageActor (IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            this.enemyView = enemyView;
            this.enemyEntity = enemyEntity;
        }

        public void ReduceHP(int damage)
        {
            enemyEntity.ReduceHP(damage);
        }

        public void Damage()
        {
            enemyView.Damage();
            enemyEntity.SetStatus(Status.Damage);
        }
        
        public void Die()
        {
            enemyView.Die();
            enemyEntity.SetStatus(Status.Die);
        }
    }
}
