using UnityEngine;
using UniRx;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerAttackController : MonoBehaviour
    {
        public void Initialize(PlayerView player, MainGameRepository mainGameRepository)
        {
            IPlayerAttackUseCase playerAttackUseCase = new PlayerAttackActor(player, mainGameRepository.Player);

            Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("Attack"))
                .Subscribe(_ => {
                    playerAttackUseCase.AttackPlayer();
                }).AddTo(this);
        }
    }
}
