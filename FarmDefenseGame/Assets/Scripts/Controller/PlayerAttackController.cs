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

        [SerializeField]
        private MainGameRepository mainGameRepository;

        // Start is called before the first frame update
        void Start()
        {
            mainGameRepository.Initialize();
            IPlayerAttackUseCase playerAttackUseCase = new PlayerAttackActor(player, mainGameRepository.Player);

            Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("Attack"))
                .Subscribe(_ => {
                    playerAttackUseCase.AttackPlayer();
                }).AddTo(this);
            
            // // アタック終わったら、プレイヤーのステータスをIDLEにする
            // // TODO: なんかもっといい設計ないのだろうか
            // Observable.EveryUpdate()
            //     .Where(_ => current_status == PlayerStatus.ATTACK)
            //     .Subscribe(_ => {
            //         if (unity_chan.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName("Standing(loop)")){
            //             SetPlayerStatus(PlayerStatus.IDLE);
            //         } 
            // }).AddTo(this);;
        }
    }
}
