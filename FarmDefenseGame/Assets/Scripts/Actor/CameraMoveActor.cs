using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class CameraMoveActor : ICameraMoveUseCase
    {
        private ICameraView cameraView;
        private ICameraEntity cameraEntity;

        public CameraMoveActor(ICameraView cameraView, ICameraEntity cameraEntity, IEnemiesView EnemyViews)
        {
            this.cameraView = cameraView;
            this.cameraEntity = cameraEntity;
            // TODO: ここでFreeのカメラ位置に初期化するようにする
        }

        public void CameraMove(float cameraInput, Vector3 playerPos)
        {
            switch (cameraEntity.CurrentCameraMode)
            {
                case CameraMode.Free:
                    cameraView.CameraMove(cameraInput, playerPos);
                break;
                case CameraMode.TargetLock:
                    SwitchTargetEnemy();
                break;
            }
        }

        private void SwitchTargetEnemy()
        {
            // TODO:今、ターゲットになっている敵よりも右or左にいる敵でかつカメラに見えてる敵にターゲットを変える処理を書く
        
        }
        
        public void InitializeCameraPos(float angleY)
        {
            cameraView.SetCameraPositionForPlayerBack(angleY);
        }

        public void SwitchCameraMode()
        {
            cameraEntity.SwitchCameraMode();

            switch (cameraEntity.CurrentCameraMode)
            {
                case CameraMode.Free:
                    // TODO: ここでターゲットロックが解除された時の処理を書く
                    // FreeのvirtualCameraを初期位置に戻す
                break;
                case CameraMode.TargetLock:
                    // TODO:ここでターゲットロックした時の処理を書く
                    // カメラに写っており、一番近い敵をVirtualCameraのlookatに設定する

                break;
            }
            
        }
    }
}

