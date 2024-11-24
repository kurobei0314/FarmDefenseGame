using UnityEngine;
using UnityEngine.InputSystem;
using R3;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle 
{
    public class PlayerAvoidController : MonoBehaviour
    {
        IPlayerAvoidUseCase _playerAvoidUseCase = null;
        public void Initialize(IPlayerView playerView, IPlayerEntity playerEntity, ICameraView cameraView)
        {
            _playerAvoidUseCase = new PlayerAvoidActor(playerView, playerEntity, cameraView);
        }

        #region InputSystemEventHandler
        public void InputAvoidEvent()
        {
            if (this.gameObject.TryGetComponent<PlayerInput>(out var playerInput))
            {
                var axis = playerInput.actions[GameInputActionName.PlayerMove].ReadValue<Vector2>();
                _playerAvoidUseCase.AvoidEnemy(axis.x, axis.y);
            }
        }
        #endregion
    }
}
