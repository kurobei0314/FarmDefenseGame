using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;
using UniRx;

namespace WolfVillageBattle
{
    public class CameraMoveController : MonoBehaviour
    {
        private PlayerView player;
        private CameraView camera;

        public void Initialize(PlayerView player, CameraView camera)
        {
            Observable.EveryUpdate()
                        .Where(_ => Input.GetAxis("CameraMove") != 0)
                        .Subscribe(_ => {
                            float cameraInput = Input.GetAxis("CameraMove");
                            camera.CameraMove(cameraInput, player.unityChan.gameObject.transform.position);
                        });
        }
    }
}

