using UnityEngine;
using WolfVillage.Entity.Interface;
using WolfVillage.Battle.Interface;

namespace WolfVillage.Battle
{
    public class PlayerNormalAttackActor : IPlayerNormalAttackUseCase
    {
        private IPlayerView playerView;
        private IBattlePlayerEntity playerEntity;
        private ICameraEntity cameraEntity;
        private IEnemiesView enemiesView;

        public PlayerNormalAttackActor(IPlayerView playerView, 
                                IBattlePlayerEntity playerEntity, 
                                ICameraEntity cameraEntity,
                                IEnemiesView enemiesView)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
            this.cameraEntity = cameraEntity;
            this.enemiesView = enemiesView;
        }
        public void AttackPlayer()
        {
            if (   playerEntity.CurrentStatus != Status.Idle 
                && playerEntity.CurrentStatus != Status.Attack
                && playerEntity.CurrentStatus != Status.JustAvoid) return;
            
            if (playerEntity.CurrentStatus == Status.JustAvoid) JustAvoidAttack();
            else                                                NormalAttack();
        }

        private void NormalAttack()
        {
            playerEntity.SetStatus(Status.Attack);
            playerView.Attack();
        }

        private void JustAvoidAttack()
        {
            playerEntity.SetStatus(Status.JustAvoidAttack);
            var targetPos = GetTargetEnemyPositionForJustAvoidAttack();
            playerView.JustAvoidAttack(targetPos);
            var timeScaler = (ITimeScaler) new TimeScaler();
            timeScaler.SetTimeScaler(1.0f);
        }

        private Vector3 GetTargetEnemyPositionForJustAvoidAttack()
        {
            switch(cameraEntity.CurrentCameraMode)
            {
                case CameraMode.Free:
                    return GetPositionForFreeCamera();
                case CameraMode.TargetLock:
                    return GetPositionForTargetLockCamera();
                default:
                    return GetPositionForFreeCamera();
            }
        }

        private Vector3 GetPositionForFreeCamera()
        {
            var enemyView = enemiesView.HitEnemyView;
            return enemyView?.Position ?? Vector3.zero;
        }

        private Vector3 GetPositionForTargetLockCamera()
        {
            var enemyView = enemiesView.TargetEnemyView;
            return enemyView?.Position ?? Vector3.zero;
        }
    }
}

