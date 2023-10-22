using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField]
        private PlayerView player;

        // Start is called before the first frame update
        public void Initialize(MainGameRepository mainGameRepository)
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
