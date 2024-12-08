using WolfVillage.Entity.Interface;
using WolfVillage.Battle.Interface;

namespace WolfVillage.Battle
{
    public class CameraMoveActor : ICameraMoveUseCase
    {
        private ICameraView cameraView;
        private ICameraEntity cameraEntity;

        public CameraMoveActor(ICameraView cameraView, ICameraEntity cameraEntity)
        {
            this.cameraView = cameraView;
            this.cameraEntity = cameraEntity;
            // TODO: ここでFreeのカメラ位置に初期化するようにする
        }

        public void CameraMove(float cameraInput, IPlayerView playerView, IEnemiesView enemiesView)
        {
            switch (cameraEntity.CurrentCameraMode)
            {
                case CameraMode.Free:
                    cameraView.CameraMove(cameraInput, playerView.unityChan.transform.position);
                    break;
                case CameraMode.TargetLock:
                    SwitchTargetEnemy(cameraInput, enemiesView, playerView);
                    break;
            }
        }

        private void SwitchTargetEnemy(float cameraInput, IEnemiesView enemyViews, IPlayerView playerView)
        {
            // TODO:今、ターゲットになっている敵よりも右or左にいる敵でかつカメラに見えてる敵にターゲットを変える処理を書く
            var nextEnemyTarget = enemyViews.GetNeighborsEnemy(cameraInput, enemyViews.TargetEnemyView, cameraView);
            if (nextEnemyTarget == null) return;
            var targetEnemy = enemyViews.TargetEnemyView;
            if (targetEnemy != null)
            {
                targetEnemy.SetTargetLockActive(false);
            }
            nextEnemyTarget.SetTargetLockActive(true);
            enemyViews.SetTargetEnemyView(nextEnemyTarget);
            cameraView.SwitchVirtualTargetLockCamera(nextEnemyTarget.GameObject.transform);
        }
        
        public void InitializeCameraPos(float angleY)
        {
            if (cameraEntity.CurrentCameraMode == CameraMode.TargetLock) return;
            cameraView.SetCameraPositionForPlayerBack(angleY);
        }

        public void SwitchCameraMode(IEnemiesView enemyViews)
        {
            var changedCameraMode = ((CameraMode)1 - (int)cameraEntity.CurrentCameraMode);
            var enemyView = enemyViews.GetMinDistanceEnemyFromPlayer(cameraView);
            switch (changedCameraMode)
            {
                case CameraMode.Free:
                    cameraView.SwitchVirtualFreeCamera();
                    cameraEntity.SetCameraMode(CameraMode.Free);
                    if (enemyView == null) return;
                    enemyView.SetTargetLockActive(false);
                    break;
                case CameraMode.TargetLock:
                    if (enemyView == null) return;
                    cameraView.SwitchVirtualTargetLockCamera(enemyView.GameObject.transform);
                    enemyViews.SetTargetEnemyView(enemyView);
                    enemyView.SetTargetLockActive(true);
                    cameraEntity.SetCameraMode(CameraMode.TargetLock);
                    break;
            }
            
        }
    }
}

