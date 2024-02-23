using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class CameraMoveActor : ICameraMoveUseCase
    {
        private ICameraView cameraView;
        public CameraMoveActor(ICameraView cameraView)
        {
            this.cameraView = cameraView;
        }

        public void CameraMove(float cameraInput, Vector3 playerPos)
        {
            cameraView.CameraMove(cameraInput, playerPos);
        }
        
        public void InitializeCameraPos(float angleY)
        {
            cameraView.SetCameraPositionForPlayerBack(angleY);
        }
    }
}

