using UnityEngine;
using WolfVillage.Battle.Interface;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle
{
    public class PlayerDamageActor : IPlayerDamageUseCase
    {
        private IPlayerView playerView;
        private IBattlePlayerEntity playerEntity;
        private IInGameView inGameView;
        private IEnemiesView enemiesView;

        public PlayerDamageActor(IPlayerView playerView, 
                                IBattlePlayerEntity playerEntity, 
                                IInGameView inGameView,
                                IEnemiesView enemiesView)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
            this.inGameView = inGameView;
            this.enemiesView = enemiesView;
        }

        public void HitEnemyAttack(Collision collision)
        {
            if (playerEntity.CurrentStatus == Status.Damage || playerEntity.CurrentStatus == Status.Die) return;
            if (playerEntity.CurrentStatus == Status.JustAvoidAttack) return;
            var enemyView = GetEnemyViewForCollision(collision);
            if (enemyView == null || enemyView.EnemyEntity.CurrentStatus != Status.Attack) return;
            enemiesView.SetHitEnemyView(enemyView);
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
            var timeScaler = (ITimeScaler) new TimeScaler();
            timeScaler.SetTimeScaler(BattleGameInfo.JUST_AVOID_TIME_SCALE);
        }

        public void Damage()
        {
            playerEntity.SetStatus(Status.Damage);
            playerView.Damage();
            inGameView.UpdatePlayerHPView((float)playerEntity.CurrentHPValue/playerEntity.CurrentMaxHP);
        }

        public void Die()
        {
            playerEntity.SetStatus(Status.Die);
            playerView.Die();
            inGameView.UpdatePlayerHPView(0);
        }
    }
    
}
