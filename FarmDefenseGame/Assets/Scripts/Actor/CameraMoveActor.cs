using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
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
            var nextEnemyTarget = enemyViews.GetNeighborsEnemy(cameraInput, cameraView.targetEnemyTrans, cameraView, playerView.unityChan.transform.right);
            if (nextEnemyTarget == null) return;
            if (cameraView.targetEnemyTrans != null)
            {
                var targetEnemy = cameraView.targetEnemyTrans.gameObject.GetComponent<IEnemyView>();
                if (targetEnemy != null) targetEnemy.SetTargetLockActive(false);
            }
            nextEnemyTarget.SetTargetLockActive(true);
            cameraView.SwitchVirtualTargetLockCamera(nextEnemyTarget.GameObject.transform);
        }
        
        public void InitializeCameraPos(float angleY)
        {
            if (cameraEntity.CurrentCameraMode == CameraMode.TargetLock) return;
            cameraView.SetCameraPositionForPlayerBack(angleY);
        }

        public void SwitchCameraMode(IEnemiesView EnemyViews)
        {
            var changedCameraMode = ((CameraMode)1 - (int)cameraEntity.CurrentCameraMode);
            var enemyView = EnemyViews.GetMinDistanceEnemyFromPlayer(cameraView);
            switch (changedCameraMode)
            {
                case CameraMode.Free:
                    Debug.Log("CameraMode.Free");
                    // TODO: ここでターゲットロックが解除された時の処理を書く
                    // FreeのvirtualCameraを初期位置に戻す
                    cameraView.SwitchVirtualFreeCamera();
                    cameraEntity.SetCameraMode(CameraMode.Free);
                    if (enemyView == null) return;
                    enemyView.SetTargetLockActive(false);
                    break;
                case CameraMode.TargetLock:
                    Debug.Log("CameraMode.TargetLock");
                    // TODO:ここでターゲットロックした時の処理を書く
                    // カメラに写っており、一番近い敵をVirtualCameraのlookatに設定する
                    if (enemyView == null) return;
                    cameraView.SwitchVirtualTargetLockCamera(enemyView.GameObject.transform);
                    enemyView.SetTargetLockActive(true);
                    cameraEntity.SetCameraMode(CameraMode.TargetLock);
                    break;
            }
            
        }
    }
}

