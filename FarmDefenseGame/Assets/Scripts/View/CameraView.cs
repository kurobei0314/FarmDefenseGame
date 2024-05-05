using UnityEngine;
using DG.Tweening;
using Cinemachine; 
using WolfVillageBattle.Interface;

public interface ICameraView
{
    Transform CameraTrans { get; }
    Transform targetEnemyTrans { get; }
    void CameraMove(float cameraInput, Vector3 playerPosition);
    void SetCameraPositionForPlayerBack(float playerAngleY);
    void SwitchVirtualFreeCamera();
    void SwitchVirtualTargetLockCamera(Transform targetEnemy);
    Vector3 CalculateViewportPointOfTargetPosition(Vector3 targetPosition);
}

public class CameraView : MonoBehaviour, ICameraView
{
    [SerializeField] private CinemachineVirtualCamera FreePosVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera TargetLockPosVirtualCamera;
    [SerializeField] private Camera mainCamera;
    public Transform CameraTrans => mainCamera.gameObject.transform;
    public Transform targetEnemyTrans => TargetLockPosVirtualCamera.LookAt;

    public void CameraMove(float cameraInput, Vector3 playerPosition)
    {
        var cameraMoveAngle = cameraInput > 0 ? 0.3f : -0.3f;
        FreePosVirtualCamera.transform.RotateAround(playerPosition, Vector3.up, cameraMoveAngle);
    }

    public void SetCameraPositionForPlayerBack(float playerAngleY)
    {
        FreePosVirtualCamera.transform.DOLocalRotate(new Vector3(0f, playerAngleY, 0f), 0.5f);
    }

    public void SwitchVirtualFreeCamera()
    {
        FreePosVirtualCamera.Priority = 10;
        TargetLockPosVirtualCamera.Priority = 0;
    }

    public void SwitchVirtualTargetLockCamera(Transform targetEnemy)
    {
        FreePosVirtualCamera.Priority = 0;
        TargetLockPosVirtualCamera.Priority = 10;
        TargetLockPosVirtualCamera.LookAt = targetEnemy;
    }

    public Vector3 CalculateViewportPointOfTargetPosition(Vector3 targetPosition)
    {
        return mainCamera.WorldToViewportPoint(targetPosition);
    }
}

