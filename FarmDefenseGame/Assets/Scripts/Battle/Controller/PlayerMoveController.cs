using UnityEngine;
using UnityEngine.InputSystem;
using R3;
using WolfVillage.Battle.Interface;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle {
    public class PlayerMoveController : MonoBehaviour
    {
        private IPlayerMoveUseCase _playerMoveUseCase;

        public void Initialize( IPlayerView player,
                                IBattlePlayerEntity playerEntity,
                                ICameraView camera,
                                PlayerInput playerInput)
        {
            _playerMoveUseCase = new PlayerMoveActor(player, playerEntity, camera);

            if (playerInput == null)
            {
                Debug.LogError("playerInputを取得できませんでした");
                return;
            }
            // プレイヤーの移動系
            Observable.EveryUpdate().Where(_ => playerInput.actions[BattleGameInputActionName.PlayerMove].IsPressed()).Subscribe(_ => {
                var axis = playerInput.actions[BattleGameInputActionName.PlayerMove].ReadValue<Vector2>();
                if (playerInput.actions[BattleGameInputActionName.PlayerWalk].IsPressed())
                {
                    // TODO: コントローラーの時の動きがおかしくなると思うのでそこを考える必要がある
                    _playerMoveUseCase.WalkPlayer(axis.x, axis.y);
                    return;
                }
                _playerMoveUseCase.RunPlayer(axis.x, axis.y);
            }).AddTo(this);

            // ボタンから離した時
            Observable.EveryUpdate().Where(_ => playerInput.actions[BattleGameInputActionName.PlayerMove].WasReleasedThisFrame()).Subscribe(_ => {
                    _playerMoveUseCase.StandPlayer();
            }).AddTo(this);
            
        }
    }
}
