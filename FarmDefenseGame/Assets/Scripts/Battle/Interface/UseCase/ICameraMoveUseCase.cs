using UnityEngine;

namespace WolfVillage.Battle.Interface
{
    public interface ICameraMoveUseCase
    {
        void CameraMove(float cameraInput, IPlayerView playerPos, IEnemiesView enemiesView);
        void InitializeCameraPos(float angleY);
        void SwitchCameraMode(IEnemiesView EnemyViews);
    }
}

