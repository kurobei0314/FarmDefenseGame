using R3;
using UnityEngine;
using WolfVillage.Battle.Interface;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle
{
    public class PlayerSkillAttackController : MonoBehaviour
    {
        IPlayerSkillAttackUseCase _playerAttackUseCase = null;
        public void Initialize(IPlayerView playerView, IBattlePlayerEntity playerEntity, IInGameView gameView)
        {
            _playerAttackUseCase = new PlayerSkillAttackActor(playerView, playerEntity, gameView);

            // MEMO: もしかしたらやってる内容的に、Presenterのクラスを作った方がいいかもしれない
            for (var i = 0; i < BattleGameInfo.PLAYER_SET_SKILL_NUM; i++)
            {
                gameView.PlayerSkillIconViews[i].AbleUseSkillObservable.Subscribe(index =>
                {
                    _playerAttackUseCase.FinishIntervalTimeSkill(index);
                });
            }
        }
        
        #region InputSystemEventHandler
        public void InputSkillAttack1Event()
            => _playerAttackUseCase.AttackPlayer(0);
        public void InputSkillAttack2Event()
            => _playerAttackUseCase.AttackPlayer(1);
        public void InputSkillAttack3Event()
            => _playerAttackUseCase.AttackPlayer(2);
        #endregion
    }
}
