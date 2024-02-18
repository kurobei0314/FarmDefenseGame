using UnityEngine;
using WolfVillageBattle.Interface;
using UniRx;

namespace WolfVillageBattle
{
    public class CameraMoveController : MonoBehaviour
    {
        public void Initialize(IPlayerView player, ICameraView camera)
        {
            Observable.EveryUpdate()
                        .Where(_ => Input.GetAxis("CameraMove") != 0)
                        .Subscribe(_ => {
                            float cameraInput = Input.GetAxis("CameraMove");
                            camera.CameraMove(cameraInput, player.unityChan.gameObject.transform.position);
                        });
            Observable.EveryUpdate()
                        .Where(_ => Input.GetAxis("InitializeCameraPos") != 0)
                        .Subscribe(_ => {
                            var angleY = player.unityChan.gameObject.transform.localEulerAngles.y;
                            camera.SetCameraPositionForPlayerBack(angleY);
                        });
        }
    }
}

