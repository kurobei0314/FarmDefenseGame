using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyNotiveActor : IEnemyNoticeUseCase
    {
        private IEnemyEntity enemyEntity;
        private IEnemyView enemyView;

        public EnemyNotiveActor (IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            this.enemyView = enemyView;
            this.enemyEntity = enemyEntity; 
        }

        public void EnemyNotice()
        {
            enemyEntity.SetStatus(Status.ANIMATION);
            enemyView.PlayNoticeSound();
            enemyView.PlayNotice();
        }
    }
}

