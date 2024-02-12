using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyNoticeActor : IEnemyNoticeUseCase
    {
        private IEnemyEntity enemyEntity;
        private IEnemyView enemyView;

        public EnemyNoticeActor (IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            this.enemyView = enemyView;
            this.enemyEntity = enemyEntity; 
        }

        public void EnemyNotice()
        {
            enemyEntity.SetStatus(Status.NOTICE);
            enemyView.Notice();
        }

        public void EnemyOverlook()
        {
            enemyEntity.SetStatus(Status.IDLE);
            enemyView.Outlook();
        }
    }
}

