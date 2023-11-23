using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    // プレイヤーのアニメーションでStatusが変更されるなどのためだけに作られたクラス
    // なんか違う気がする
    public class EnemyStatusView : MonoBehaviour
    {
        private IEnemyEntity enemyEntity;

        public void Initialize (IEnemyEntity enemyEntity)
        {
            this.enemyEntity = enemyEntity;
        }
        
        public void SetNoticeStatus()
        {
            if (enemyEntity.CurrentStatus != Status.ANIMATION) return;
            enemyEntity.SetStatus(Status.NOTICE);
        }

        public void SetIdleStatus()
        {
            if (enemyEntity.CurrentStatus != Status.ANIMATION) return;
            enemyEntity.SetStatus(Status.IDLE);
        }
    }
}

