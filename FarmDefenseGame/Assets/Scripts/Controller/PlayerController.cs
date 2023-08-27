using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerController : MonoBehaviour
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
            mainGameRepository.Initialize();
            IPlayerUseCase playerUseCase = new PlayerActor(player, mainGameRepository.Player, camera);
            // var moveDownStream = this.UpdateAsObservable().Where(_ =>   Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f ||
            //                                                             Input.GetKey(KeyCode.UpArrow)    ||  Input.GetKey(KeyCode.DownArrow) ||
            //                                                             Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow));

            // var moveUpStream = this.UpdateAsObservable().Where(_ =>   (Input.GetAxis("Horizontal") == 0.0f && Input.GetAxis("Vertical") == 0.0f) ||
            //                                                             Input.GetKeyUp(KeyCode.UpArrow)    ||  Input.GetKeyUp(KeyCode.DownArrow) || 
            //                                                             Input.GetKeyUp(KeyCode.RightArrow) ||  Input.GetKeyUp(KeyCode.LeftArrow));
            // プレイヤーの移動系
            Observable.EveryUpdate()
                .Where(_ => Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f ||
                            Input.GetKey(KeyCode.UpArrow)    ||  Input.GetKey(KeyCode.DownArrow) ||
                            Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                .Subscribe(_ => {
                    float horizontalInput = Input.GetAxis("Horizontal");
                    float verticalInput = Input.GetAxis("Vertical");
                    playerUseCase.MovePlayer(horizontalInput, verticalInput);
            }).AddTo(this);

            // ボタンから離した時
            Observable.EveryUpdate()
                .Where(_ => (Input.GetAxis("Horizontal") == 0.0f && Input.GetAxis("Vertical") == 0.0f) ||
                            Input.GetKeyUp(KeyCode.UpArrow)    ||  Input.GetKeyUp(KeyCode.DownArrow) || 
                            Input.GetKeyUp(KeyCode.RightArrow) ||  Input.GetKeyUp(KeyCode.LeftArrow))
                .Subscribe(_ => {
                    playerUseCase.StandPlayer();
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
