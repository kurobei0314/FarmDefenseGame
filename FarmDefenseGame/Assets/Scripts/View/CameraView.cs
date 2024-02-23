using UnityEngine;
using DG.Tweening;
using Cinemachine; 
using WolfVillageBattle.Interface;

public interface ICameraView
{
    Transform CameraTrans { get; }
    public void CameraMove(float cameraInput, Vector3 playerPosition);
    public void SetCameraPositionForPlayerBack(float playerAngleY);
}

public class CameraView : MonoBehaviour, ICameraView
{
    [SerializeField] private CinemachineVirtualCamera FreePosVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera TargetLockPosVirtualCamera;
    public Transform CameraTrans => Camera.main.gameObject.transform;

    public void CameraMove(float cameraInput, Vector3 playerPosition)
    {
        var cameraMoveAngle = cameraInput > 0 ? 0.3f : -0.3f;
        FreePosVirtualCamera.transform.RotateAround(playerPosition, Vector3.up, cameraMoveAngle);
    }

    public void SetCameraPositionForPlayerBack(float playerAngleY)
    {
        FreePosVirtualCamera.transform.DOLocalRotate(new Vector3(0f, playerAngleY, 0f), 0.5f);
    }

    public void SwitchVirtualCamera(CameraMode cameraMode)
    {
        switch (cameraMode)
        {
            case CameraMode.Free:
                // 
                FreePosVirtualCamera.Priority = 10;
                TargetLockPosVirtualCamera.Priority = 0;
            break;
            case CameraMode.TargetLock:
                // TODO: ここでTargetLock
                FreePosVirtualCamera.Priority = 0;
                TargetLockPosVirtualCamera.Priority = 10;
            break;
        }
    }
}

