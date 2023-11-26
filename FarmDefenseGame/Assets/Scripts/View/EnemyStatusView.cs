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
        private EnemyMoveAI enemyMoveAI;
        private IEnemyEntity enemyEntity;

        public void Initialize (EnemyMoveAI enemyMoveAI, IEnemyEntity enemyEntity)
        {
            this.enemyEntity = enemyEntity;
            this.enemyMoveAI = enemyMoveAI;
        }
        
        public void SetNoticeStatus()
        {
            enemyEntity.SetStatus(Status.NOTICE);
            enemyMoveAI.StartNavMesh();
        }

        public void SetIdleStatus()
        {
            enemyEntity.SetStatus(Status.IDLE);
            enemyMoveAI.StartNavMesh();
        }
    }
}

