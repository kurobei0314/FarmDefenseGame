namespace WolfVillageBattle.Interface
{
    public enum CameraMode
    {
        Free,
        TargetLock,
    }

    public interface ICameraEntity 
    {
        CameraMode CurrentCameraMode { get; }
        public void SetCameraMode (CameraMode cameraMode);
    }
}
