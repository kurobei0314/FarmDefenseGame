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
        public void SwitchCameraMode();
    }
}
