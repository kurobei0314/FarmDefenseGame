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
        }

        public void CameraMove(float cameraInput, Vector3 playerPos)
        {
            cameraView.CameraMove(cameraInput, playerPos);
        }
        
        public void InitializeCameraPos(float angleY)
        {
            cameraView.SetCameraPositionForPlayerBack(angleY);
        }

        public void SwitchCameraMode()
        {
            cameraEntity.SwitchCameraMode();
        }
    }
}

