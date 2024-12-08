using WolfVillage.Entity.Interface;

namespace WolfVillage.Entity
{
    public class CameraEntity : ICameraEntity
    {
        public CameraEntity()
        {
            current_camera_mode = CameraMode.Free;
        }

        private CameraMode current_camera_mode;
        public CameraMode CurrentCameraMode => current_camera_mode;

        public void SetCameraMode (CameraMode cameraMode)
        {
            current_camera_mode = cameraMode;
        }
    }
}

