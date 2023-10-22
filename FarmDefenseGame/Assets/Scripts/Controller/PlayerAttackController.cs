using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerAttackController : MonoBehaviour
    {
        private PlayerView player;

        // Start is called before the first frame update
        public void Initialize(PlayerView player, MainGameRepository mainGameRepository)
        {
            this.player = player;
            IPlayerAttackUseCase playerAttackUseCase = new PlayerAttackActor(player, mainGameRepository.Player);

            Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("Attack"))
                .Subscribe(_ => {
                    playerAttackUseCase.AttackPlayer();
                }).AddTo(this);
        }
    }
}
