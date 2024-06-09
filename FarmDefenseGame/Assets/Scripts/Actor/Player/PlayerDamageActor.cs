using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerDamageActor : IPlayerDamageUseCase
    {
        private IPlayerView playerView;
        private IPlayerEntity playerEntity;

        public PlayerDamageActor(IPlayerView playerView, IPlayerEntity playerEntity)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
        }

        public void HitEnemyAttack(Collision collision)
        {
            if (playerEntity.CurrentStatus == Status.Damage || playerEntity.CurrentStatus == Status.Die) return;
            var enemyView = GetEnemyViewForCollision(collision);
            if (enemyView == null || enemyView.EnemyEntity.CurrentStatus != Status.Attack) return;
            if (playerEntity.CurrentStatus == Status.Avoid) AvoidEnemyAttack();
            else                                            ReduceHP(enemyView.EnemyEntity.EnemyVO.Attack);
        }

        private EnemyView GetEnemyViewForCollision(Collision collision)
        {
            var parent = collision.gameObject.transform.parent;
            if (parent == null || parent.gameObject.tag != "Enemy") return null;
            var enemyView = parent.gameObject.GetComponent<EnemyView>();
            if (!enemyView) 
            {
                Debug.LogError("enemyViewが取得できませんでした");
                return null;
            }
            return enemyView;
        }

        private void ReduceHP (int damage)
        {
            playerEntity.ReduceHP(damage);
        }

        private void AvoidEnemyAttack()
        {
            playerEntity.SetStatus(Status.JustAvoid);
            var timeScaler = new TimeScaler();
            timeScaler.SetTimeScaler(GameInfo.JUST_AVOID_TIME_SCALE);
        }

        public void Damage()
        {
            playerEntity.SetStatus(Status.Damage);
            playerView.Damage((float)playerEntity.CurrentHPValue/playerEntity.PlayerVO.MaxHP);
        }

        public void Die()
        {
            playerEntity.SetStatus(Status.Die);
            playerView.Die();
        }
    }
    
}
