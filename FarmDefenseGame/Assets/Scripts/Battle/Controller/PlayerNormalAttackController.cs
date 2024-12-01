using UnityEngine;
using R3;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerNormalAttackController : MonoBehaviour
    {
        private IPlayerNormalAttackUseCase _playerNormalAttackUseCase = null;
        public void Initialize(IPlayerView player,
                                IPlayerEntity playerEntity,
                                ICameraEntity cameraEntity,
                                IEnemiesView enemiesView)
        {
            _playerNormalAttackUseCase = new PlayerNormalAttackActor(player, playerEntity, cameraEntity, enemiesView);
        }

        #region InputSystemEventHandler
        public void InputNormalAttackEvent()
            => _playerNormalAttackUseCase.AttackPlayer();
        #endregion
    }
}
