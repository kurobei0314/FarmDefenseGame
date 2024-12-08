using UnityEngine;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle
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
            enemyEntity.SetStatus(Status.Notice);
            enemyMoveAI.StartNavMesh();
        }

        public void SetIdleStatus()
        {
            enemyEntity.SetStatus(Status.Idle);
            enemyMoveAI.StartNavMesh();
        }

        public void DestroyAfterDieAnimation()
        {
            Destroy(this.gameObject);
        }
    }
}

