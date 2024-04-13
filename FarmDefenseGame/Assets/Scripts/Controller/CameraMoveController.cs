using UnityEngine;
using WolfVillageBattle.Interface;
using UniRx;

namespace WolfVillageBattle
{
    public class CameraMoveController : MonoBehaviour
    {
        public void Initialize(IPlayerView player, ICameraView cameraView, ICameraEntity cameraEntity, IEnemiesView enemyViews)
        {
            ICameraMoveUseCase cameraMoveUseCase = new CameraMoveActor(cameraView, cameraEntity);

            Observable.EveryUpdate()
                        .Where(_ => Input.GetAxis("CameraMove") != 0)
                        .Subscribe(_ => {
                            float cameraInput = Input.GetAxis("CameraMove");
                            cameraMoveUseCase.CameraMove(cameraInput, player.unityChan.gameObject.transform.position);
                        }).AddTo(this);
            
            Observable.EveryUpdate()
                        .Where(_ => Input.GetButtonDown("InitializeCameraPos"))
                        .Subscribe(_ => {
                            var angleY = player.unityChan.gameObject.transform.localEulerAngles.y;
                            cameraMoveUseCase.InitializeCameraPos(angleY);
                        }).AddTo(this);

            Observable.EveryUpdate()
                        .Where(_ => Input.GetButtonDown("SwitchCameraTargetLock"))
                        .Subscribe(_ => {
                            cameraMoveUseCase.SwitchCameraMode(enemyViews);
                        }).AddTo(this);
        }
    }
}

