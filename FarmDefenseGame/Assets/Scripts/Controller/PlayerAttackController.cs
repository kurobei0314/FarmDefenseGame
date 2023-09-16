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
        private CameraView camera;

        [SerializeField]
        private MainGameRepository mainGameRepository;

        // Start is called before the first frame update
        void Start()
        {
            // // アタック終わったら、プレイヤーのステータスをIDLEにする
            // // TODO: なんかもっといい設計ないのだろうか
            // Observable.EveryUpdate()
            //     .Where(_ => current_status == PlayerStatus.ATTACK)
            //     .Subscribe(_ => {
            //         if (unity_chan.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName("Standing(loop)")){
            //             SetPlayerStatus(PlayerStatus.IDLE);
            //         } 
            // }).AddTo(this);;

            // // 攻撃モーション
            // Observable.EveryUpdate()
            //     .Where(_ => Input.GetButtonDown("Attack"))
            //     .Subscribe(_ => {
            //         if (CurrentStatus != PlayerStatus.IDLE && CurrentStatus != PlayerStatus.ATTACK) return;
            //         SetPlayerStatus(PlayerStatus.ATTACK);
            //         float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
            //             if (rnd < 0.5f){
            //                 AudioManager.Instance.PlaySE("unitychan_attack1");
            //             } else{
            //                 AudioManager.Instance.PlaySE("unitychan_attack2");
            //             }
            //         // MEMO: 攻撃のモーションがなかったのでとりあえずこれで仮置き
            //         unity_chan.GetComponent<Animator>().Play("KneelDownToUp");
            // }).AddTo(this);
        }
    }
}
