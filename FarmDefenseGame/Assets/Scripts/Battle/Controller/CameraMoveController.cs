using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Battle.Interface;
using R3;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle
{
    public class CameraMoveController : MonoBehaviour
    {
        IPlayerView _playerView = null;
        IEnemiesView _enemyViews = null;
        ICameraMoveUseCase _cameraMoveUseCase = null;

        public void Initialize( IPlayerView player,
                                ICameraView cameraView,
                                ICameraEntity cameraEntity,
                                IEnemiesView enemyViews,
                                PlayerInput playerInput)
        {
            _playerView = player;
            _enemyViews = enemyViews;
            _cameraMoveUseCase = new CameraMoveActor(cameraView, cameraEntity);

            if (playerInput == null)
            {
                Debug.LogError("playerInputを取得できませんでした");
                return;
            }

            // カメラの移動系
            Observable.EveryUpdate()
                        .Where(_ =>    playerInput.actions[BattleGameInputActionName.CameraMove].IsPressed() 
                                    && cameraEntity.CurrentCameraMode == CameraMode.Free)
                        .Subscribe(_ => {
                            var cameraInput = playerInput.actions[BattleGameInputActionName.CameraMove].ReadValue<float>();
                            _cameraMoveUseCase.CameraMove(cameraInput, player, enemyViews);
                        }).AddTo(this);

            Observable.EveryUpdate()
                        .Where(_ =>    playerInput.actions[BattleGameInputActionName.CameraMove].WasPressedThisFrame()
                                    && cameraEntity.CurrentCameraMode == CameraMode.TargetLock)
                        .Subscribe(_ => {
                            var cameraInput = playerInput.actions[BattleGameInputActionName.CameraMove].ReadValue<float>();
                            _cameraMoveUseCase.CameraMove(cameraInput, player, enemyViews);
                        }).AddTo(this);
        }

        #region InputSystemEventHandler
        public void InputInitCameraPosEvent()
        {
            var angleY = _playerView.unityChan.gameObject.transform.localEulerAngles.y;
            _cameraMoveUseCase.InitializeCameraPos(angleY);
        }
        public void InputSwitchCameraMode()
            => _cameraMoveUseCase.SwitchCameraMode(_enemyViews);
        #endregion
    }
}

