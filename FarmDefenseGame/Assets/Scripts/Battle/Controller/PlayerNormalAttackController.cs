using UnityEngine;
using WolfVillage.Battle.Interface;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle {
    public class PlayerNormalAttackController : MonoBehaviour
    {
        private IPlayerNormalAttackUseCase _playerNormalAttackUseCase = null;
        public void Initialize(IPlayerView player,
                                IBattlePlayerEntity playerEntity,
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
