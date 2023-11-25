using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyAttackActor : IEnemyAttackUseCase
    {
        private IEnemyView enemyView;
        private IEnemyEntity enemyEntity;

        public EnemyAttackActor (IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            this.enemyView = enemyView;
            this.enemyEntity = enemyEntity;
        }

        public void StartAttack()
        {
            if (enemyEntity.CurrentStatus != Status.ATTACK) return;
            enemyEntity.SetStatus(Status.ATTACK);
            enemyView.PlayAttack();
        }
        
        public void StopAttack()
        {
            enemyEntity.SetStatus(Status.NOTICE);
        }
    }
}
