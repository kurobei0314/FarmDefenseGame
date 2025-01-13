using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Battle.Interface;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle 
{
    public class PlayerAvoidController : MonoBehaviour
    {
        IPlayerAvoidUseCase _playerAvoidUseCase = null;
        PlayerInput _playerInput = null;
        public void Initialize( IPlayerView playerView,
                                IPlayerEntity playerEntity,
                                ICameraView cameraView,
                                PlayerInput playerInput)
        {
            _playerAvoidUseCase = new PlayerAvoidActor(playerView, playerEntity, cameraView);
            _playerInput = playerInput;
        }

        #region InputSystemEventHandler
        public void InputAvoidEvent()
        {
            if (_playerInput == null) return;
            var axis = _playerInput.actions[BattleGameInputActionName.PlayerMove].ReadValue<Vector2>();
            _playerAvoidUseCase.AvoidEnemy(axis.x, axis.y);
            
        }
        #endregion
    }
}
