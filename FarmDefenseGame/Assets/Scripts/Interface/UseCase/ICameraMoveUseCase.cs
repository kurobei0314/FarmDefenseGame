using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface ICameraMoveUseCase
    {
        void CameraMove(float cameraInput, Vector3 playerPos);
        void InitializeCameraPos(float angleY);
    }
}

