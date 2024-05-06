using UnityEngine;
using DG.Tweening;
using Cinemachine; 
using WolfVillageBattle.Interface;
using System;

public interface ICameraView
{
    Transform CameraTrans { get; }
    Transform targetEnemyTrans { get; }
    void CameraMove(float cameraInput, Vector3 playerPosition);
    void SetCameraPositionForPlayerBack(float playerAngleY);
    void SwitchVirtualFreeCamera();
    void SwitchVirtualTargetLockCamera(Transform targetEnemy);
    Boolean IsVisibleInCamera(Vector3 targetPosition);
}

public class CameraView : MonoBehaviour, ICameraView
{
    [SerializeField] private CinemachineVirtualCamera freePosVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera targetLockPosVirtualCamera;
    // TargetGroupには以下が設定されている
    // 0番目: 操作プレイヤー
    // 1番目: ターゲットロックされたエネミー(切り替え可能)
    [SerializeField] private CinemachineTargetGroup  targetLockCinemachineTargetGroup;
    [SerializeField] private Camera mainCamera;
    public Transform CameraTrans => mainCamera.gameObject.transform;
    public Transform targetEnemyTrans => targetLockPosVirtualCamera.LookAt;

    public void CameraMove(float cameraInput, Vector3 playerPosition)
    {
        var cameraMoveAngle = cameraInput > 0 ? 0.3f : -0.3f;
        freePosVirtualCamera.transform.RotateAround(playerPosition, Vector3.up, cameraMoveAngle);
    }

    public void SetCameraPositionForPlayerBack(float playerAngleY)
    {
        freePosVirtualCamera.transform.DOLocalRotate(new Vector3(0f, playerAngleY, 0f), 0.5f);
    }

    public void SwitchVirtualFreeCamera()
    {
        freePosVirtualCamera.Priority = 10;
        targetLockPosVirtualCamera.Priority = 0;
    }

    public void SwitchVirtualTargetLockCamera(Transform targetEnemy)
    {
        freePosVirtualCamera.Priority = 0;
        targetLockPosVirtualCamera.Priority = 10;
        targetLockCinemachineTargetGroup.m_Targets[1].target = targetEnemy;
    }

    public Boolean IsVisibleInCamera(Vector3 targetPosition)
    {
        var viewPos = mainCamera.WorldToViewportPoint(targetPosition);
        return (viewPos.x >= 0 && viewPos.x <=1 && viewPos.y >= 0 && viewPos.y <=1 && viewPos.z >=0) ? true : false;
    }
}

