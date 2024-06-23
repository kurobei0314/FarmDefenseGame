using UniRx;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerSkillAttackController : MonoBehaviour
    {
        public PlayerSkillAttackController(IPlayerView playerView, IPlayerEntity playerEntity, IInGameView gameView)
        {
            IPlayerSkillAttackUseCase playerAttackUseCase = new PlayerSkillAttackActor(playerView, playerEntity, gameView);

                Observable.EveryUpdate()
                    .Where(_ => Input.GetButtonDown("SkillAttack1"))
                    .Subscribe(_ => {
                        playerAttackUseCase.AttackPlayer(0);
                    }).AddTo(this);

                Observable.EveryUpdate()
                    .Where(_ => Input.GetButtonDown("SkillAttack2"))
                    .Subscribe(_ => {
                        playerAttackUseCase.AttackPlayer(1);
                    }).AddTo(this);
                
                Observable.EveryUpdate()
                    .Where(_ => Input.GetButtonDown("SkillAttack3"))
                    .Subscribe(_ => {
                        playerAttackUseCase.AttackPlayer(2);
                    }).AddTo(this);
        }
    }
}
