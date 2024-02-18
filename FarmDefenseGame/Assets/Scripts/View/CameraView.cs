using UnityEngine;
using DG.Tweening;

public interface ICameraView
{
    Transform CameraParentTrans { get; }
    Transform CameraTrans { get; }
    public void CameraMove(float cameraInput, Vector3 playerPosition);
    public void SetCameraPositionForPlayerBack(float playerAngleY);
}

public class CameraView : MonoBehaviour, ICameraView
{
    [SerializeField] private GameObject cameraGo;
    public Transform CameraParentTrans => this.gameObject.transform;
    public Transform CameraTrans => cameraGo.transform;

    public void CameraMove(float cameraInput, Vector3 playerPosition)
    {
        var cameraMoveAngle = cameraInput > 0 ? 0.3f : -0.3f;
        CameraParentTrans.RotateAround(playerPosition, Vector3.up, cameraMoveAngle);
    }

    public void SetCameraPositionForPlayerBack(float playerAngleY)
    {
        CameraParentTrans.DOLocalRotate(new Vector3(0f, playerAngleY, 0f), 0.5f);
    }
}

