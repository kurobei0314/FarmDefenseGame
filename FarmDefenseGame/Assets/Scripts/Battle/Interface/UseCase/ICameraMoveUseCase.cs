using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface ICameraMoveUseCase
    {
        void CameraMove(float cameraInput, IPlayerView playerPos, IEnemiesView enemiesView);
        void InitializeCameraPos(float angleY);
        void SwitchCameraMode(IEnemiesView EnemyViews);
    }
}

