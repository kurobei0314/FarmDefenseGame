using UnityEngine;
using WolfVillageBattle.Interface;
using UniRx;

namespace WolfVillageBattle
{
    public class CameraMoveController : MonoBehaviour
    {
        public void Initialize(IPlayerView player, ICameraView camera)
        {
            ICameraMoveUseCase cameraMoveUseCase = new CameraMoveActor(camera);

            Observable.EveryUpdate()
                        .Where(_ => Input.GetAxis("CameraMove") != 0)
                        .Subscribe(_ => {
                            float cameraInput = Input.GetAxis("CameraMove");
                            cameraMoveUseCase.CameraMove(cameraInput, player.unityChan.gameObject.transform.position);
                        }).AddTo(this);
            
            Observable.EveryUpdate()
                        .Where(_ => Input.GetAxis("InitializeCameraPos") != 0)
                        .Subscribe(_ => {
                            var angleY = player.unityChan.gameObject.transform.localEulerAngles.y;
                            cameraMoveUseCase.InitializeCameraPos(angleY);
                        }).AddTo(this);

            Observable.EveryUpdate()
                        .Where(_ => Input.GetAxis("SwitchCameraTargetLock") != 0)
                        .Subscribe(_ => {

                        }).AddTo(this);
        }
    }
}

