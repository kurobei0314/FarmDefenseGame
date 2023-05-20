using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerTest : MonoBehaviour
{
    // TODO: enumで状態管理するのも良くないのかもしれないし、いいのかもしれない
    public enum PlayerStatus
    {
        IDLE,
        ATTACK,
        DAMAGE,
    }

    public PlayerStatus CurrentStatus => current_status;
    private PlayerStatus current_status;

    // unityちゃんのモデル
    [SerializeField] GameObject unity_chan;
    // カメラ
    [SerializeField] GameObject camera;

    // Start is called before the first frame update
    // MEMO: 大体ここにプレイヤーが操作するものが入ってる
    void Start()
    {
        // 初期の回転のやつを保持
        Quaternion default_rotation = unity_chan.gameObject.transform.rotation;

        // プレイヤーの移動系
        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                        Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            .Subscribe(_ => {
                Vector3 pos = this.transform.position;
                if        (Input.GetKey(KeyCode.UpArrow)){
                    this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(0.0f, 0.0f, 0.05f));
                } else if (Input.GetKey(KeyCode.DownArrow)){
                    this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(0.0f, 0.0f, -0.05f));
                } else if (Input.GetKey(KeyCode.RightArrow)){
                    this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(0.05f, 0.0f, 0.0f));
                } else if (Input.GetKey(KeyCode.LeftArrow)){
                    this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(-0.05f, 0.0f, 0.0f));
                }
                unity_chan.GetComponent<Animator>().Play("Running(loop)");
        });

        // ボタンから離した時
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyUp(KeyCode.UpArrow)    ||  Input.GetKeyUp(KeyCode.DownArrow) || 
                        Input.GetKeyUp(KeyCode.RightArrow) ||  Input.GetKeyUp(KeyCode.LeftArrow))
            .Subscribe(_ => {
                unity_chan.GetComponent<Animator>().Play("Standing(loop)");
        });

        // ボタンを押した時、モデルだけが回転する
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.UpArrow)    ||  Input.GetKeyDown(KeyCode.DownArrow) || 
                        Input.GetKeyDown(KeyCode.RightArrow) ||  Input.GetKeyDown(KeyCode.LeftArrow))
            .Subscribe(_ => {
                unity_chan.gameObject.transform.rotation = default_rotation;
                if        (Input.GetKeyDown(KeyCode.UpArrow)) {
                    return;
                } else if (Input.GetKeyDown(KeyCode.DownArrow)){
                    unity_chan.transform.Rotate(0.0f, 180.0f, 0.0f);
                } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    unity_chan.transform.Rotate(0.0f, 90.0f, 0.0f);
                } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    unity_chan.transform.Rotate(0.0f, -90.0f, 0.0f);
                } 
        });

        // アタック終わったら、プレイヤーのステータスをIDLEにする
        // TODO: なんかもっといい設計ないのだろうか
        Observable.EveryUpdate()
            .Where(_ => current_status == PlayerStatus.ATTACK)
            .Subscribe(_ => {
                if (unity_chan.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName("Standing(loop)")){
                    SetPlayerStatus(PlayerStatus.IDLE);
                } 
        });

        // 攻撃モーション
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.W))
            .Subscribe(_ => {
                SetPlayerStatus(PlayerStatus.ATTACK);
                // MEMO: 攻撃のモーションがなかったのでとりあえずこれで仮置き
                unity_chan.GetComponent<Animator>().Play("KneelDownToUp");
        });

        // カメラの移動
        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            .Subscribe(_ => {
                if        (Input.GetKey(KeyCode.A)) {
                    camera.transform.RotateAround(unity_chan.gameObject.transform.position, Vector3.up, -0.1f);
                } else if (Input.GetKey(KeyCode.D)) {
                    camera.transform.RotateAround(unity_chan.gameObject.transform.position, Vector3.up, 0.1f);
                }
        });

        // 敵からの攻撃を受けた時の挙動
        this.OnTriggerEnterAsObservable()
            .Subscribe(_ => {
                var parent = _.gameObject.transform.parent;
                if (parent == null || parent.gameObject.tag != "Enemy") return;
                if (CurrentStatus == PlayerStatus.ATTACK) return;
                SetPlayerStatus(PlayerStatus.DAMAGE);
                Debug.Log("敵から攻撃されたおー");
                unity_chan.GetComponent<Animator>().Play("Damaged(loop)");
        });
    }
    
    /// <summary>
    /// playerが攻撃アニメーションする時のやつ
    /// </summary>
    // private IEnumerator AttackAnimationFlow()
    // {
    //     SetPlayerStatus(PlayerStatus.ATTACK);
    //     // MEMO: 攻撃のモーションがなかったのでとりあえずこれで仮置き
    //     GetComponent<Animator>().Play("KneelDownToUp");
    //     yield return null; // ステートの反映に1フレームいるらしい。
    //     anim_hash = animator.GetCurrentAnimatorStateInfo (0).fullPathHash;
    //     SetPlayerStatus(PlayerStatus.IDLE);
    // }

    /// <summary>
    /// PlayerStatusをセットする
    /// </summary>
    private void SetPlayerStatus(PlayerStatus status)
    {
        current_status = status;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
