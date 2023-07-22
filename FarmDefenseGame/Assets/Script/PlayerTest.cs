using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class PlayerTest : MonoBehaviour
{
    // TODO: enumで状態管理するのも良くないのかもしれないし、いいのかもしれない
    public enum PlayerStatus
    {
        IDLE,
        ATTACK,
        DAMAGE,
        DIE,
        WIN, // これはいらない？
    }

    public PlayerStatus CurrentStatus => current_status;
    private PlayerStatus current_status;
    public int HP => hp;
    private int max_hp = 9;
    private int hp;

    private int attack = 1;
    public int Attack => attack;


    // unityちゃんのモデル
    [SerializeField] GameObject unity_chan;
    // カメラ
    [SerializeField] GameObject camera;

    [SerializeField] Slider HPbar;

    // 自分が死んだ時のObservable(ゲームダンジョン用)
    private Subject<int> playerDie = new Subject<int>();
    public IObservable<int> PlayerDieObservable => playerDie;

    // Start is called before the first frame update
    // MEMO: 大体ここにプレイヤーが操作するものが入ってる
    void Start()
    {
        AudioManager.Instance.PlaySE("unitychan_begin");
        hp = max_hp;
        UpdateHPbar(1.0f);

        // プレイヤーの移動系
        Observable.EveryUpdate()
            .Where(_ => Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f ||
                        Input.GetKey(KeyCode.UpArrow)    ||  Input.GetKey(KeyCode.DownArrow) ||
                        Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            .Subscribe(_ => {
                if (CurrentStatus != PlayerStatus.IDLE) return;
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                // Debug.Log("horizontalInput: " + horizontalInput);
                // Debug.Log("verticalInput: " + verticalInput);
                Vector3 pos = this.transform.position;
                Vector3 moveDirection = (camera.transform.forward * verticalInput + camera.transform.right * horizontalInput).normalized;
                this.GetComponent<Rigidbody>().MovePosition(pos + moveDirection * 0.07f);
                unity_chan.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                unity_chan.GetComponent<Animator>().Play("Running(loop)");
        }).AddTo(this);

        // ボタンから離した時
        Observable.EveryUpdate()
            .Where(_ => (Input.GetAxis("Horizontal") == 0.0f && Input.GetAxis("Vertical") == 0.0f) ||
                        Input.GetKeyUp(KeyCode.UpArrow)    ||  Input.GetKeyUp(KeyCode.DownArrow) || 
                        Input.GetKeyUp(KeyCode.RightArrow) ||  Input.GetKeyUp(KeyCode.LeftArrow))
            .Subscribe(_ => {
                if (CurrentStatus != PlayerStatus.IDLE) return;
                unity_chan.GetComponent<Animator>().Play("Standing(loop)");
        }).AddTo(this);

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
            .Where(_ => Input.GetButtonDown("Attack"))
            .Subscribe(_ => {
                if (CurrentStatus != PlayerStatus.IDLE && CurrentStatus != PlayerStatus.ATTACK) return;
                SetPlayerStatus(PlayerStatus.ATTACK);
                float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (rnd < 0.5f){
                        AudioManager.Instance.PlaySE("unitychan_attack1");
                    } else{
                        AudioManager.Instance.PlaySE("unitychan_attack2");
                    }
                // MEMO: 攻撃のモーションがなかったのでとりあえずこれで仮置き
                unity_chan.GetComponent<Animator>().Play("KneelDownToUp");
        }).AddTo(this);

        // カメラの移動
        Observable.EveryUpdate()
            .Where(_ => Input.GetAxis("CameraMove") != 0)
            .Subscribe(_ => {
                float cameraInput = Input.GetAxis("CameraMove");
                Debug.Log("CameMove: " + cameraInput);
                camera.transform.RotateAround(unity_chan.gameObject.transform.position, Vector3.up, cameraInput * 0.7f);
        }).AddTo(this);

        // 敵からの攻撃を受けた時の挙動
        this.OnTriggerEnterAsObservable()
            .Subscribe(_ => {
                var parent = _.gameObject.transform.parent;
                if (parent == null || parent.gameObject.tag != "Enemy") return;
                // TODO：これも絶対仕組み変える絶対に
                if (CurrentStatus != PlayerStatus.IDLE) return;
                var enemy_status = parent.gameObject.GetComponent<EnemyTest>().current_enemy_status;
                if (enemy_status != EnemyTest.EnemyStatus.ATTACK) return;
                Debug.Log("敵から攻撃されたおー");
                ReserveDamage(parent.gameObject.GetComponent<EnemyTest>().Attack);
                UpdateHPbar(hp/(float)max_hp);
                if (hp == 0) {
                    SetPlayerStatus(PlayerStatus.DIE);
                    Debug.Log("HPが0になっちゃったー");
                    AudioManager.Instance.PlaySE("unitychan_lose");
                    unity_chan.GetComponent<Animator>().Play("GoDown");
                    playerDie.OnNext(1);
                }
                else {
                    SetPlayerStatus(PlayerStatus.DAMAGE);
                    float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (rnd < 0.5f){
                        AudioManager.Instance.PlaySE("unitychan_damage1");
                    } else{
                        AudioManager.Instance.PlaySE("unitychan_damage2");
                    }
                    unity_chan.GetComponent<Animator>().Play("Damaged(loop)");
                }
        }).AddTo(this);

        // ダメージ終わったら、プレイヤーのステータスをIDLEにする
        // TODO: なんかもっといい設計ないのだろうか
        Observable.EveryUpdate()
            .Where(_ => CurrentStatus == PlayerStatus.DAMAGE)
            .Subscribe(_ => {
                if (unity_chan.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName("Standing(loop)")){
                    SetPlayerStatus(PlayerStatus.IDLE);
                } 
        }).AddTo(this);
    }

    /// <summary>
    /// プレイヤーがクリアした時に実行する
    /// MEMO: 東京ゲームダンジョン用
    /// </summary>
    public void GameClearPlayer()
    {
        SetPlayerStatus(PlayerStatus.WIN);
        float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
        if (rnd < 0.5f){
            AudioManager.Instance.PlaySE("unitychan_win1");
        } else {
            AudioManager.Instance.PlaySE("unitychan_win2");
        }
        Vector3 playerToCamera = camera.transform.position - unity_chan.transform.position;
        float dotProduct = Vector3.Dot(unity_chan.transform.forward, playerToCamera.normalized);
        Vector3[] paths;
        // 正面を向いてる場合
        // TODO: ここもちゃんとリファクタリングする
        if (dotProduct > 0.0f){
            Vector3[] tmp_paths = {
                GetCameraPositionForPlayer(new Vector3(-0.03f, 1.42f, 2.73f)), // 最終地点
            };
            paths = tmp_paths;
        } else {
            Vector3[] tmp_paths = {
                GetCameraPositionForPlayer(new Vector3(2.06f, 0.96f, -0.09f)),  // キャラがカメラの方に向いてる時に死ぬので考える
                GetCameraPositionForPlayer(new Vector3(-0.03f, 1.42f, 2.73f)), // 最終地点
            };
            paths = tmp_paths;
        }
        camera.transform.DOLocalPath(paths, 2.0f, PathType.CatmullRom).SetLookAt(unity_chan.transform, stableZRotation: true);
        unity_chan.GetComponent<Animator>().Play("Jumping(loop)");
    }

    /// <summary>
    /// プレイヤーの場所に合わせたカメラ移動をするときの位置を求める
    /// MEMO: 東京ゲームダンジョン用
    /// </summary>
    private Vector3 GetCameraPositionForPlayer(Vector3 offset){
        return unity_chan.transform.forward * offset.z + unity_chan.transform.up * offset.y + unity_chan.transform.right * offset.x;
    }

    /// <summary>
    /// PlayerStatusをセットする
    /// </summary>
    private void SetPlayerStatus(PlayerStatus status)
    {
        current_status = status;
    }

    /// <summary>
    /// HPを減少させる
    /// </summary> 
    private void ReserveDamage(int value){
        if( hp - value < 0 ){
            hp = 0;
        }
        else{
            hp -= value;
        }
    }

    private void UpdateHPbar(float value){
        HPbar.value = value;
    }

    // /// <summary>
    // /// 勝った時のカメラの移動
    // /// </summary>
    // /// <param name="value"></param>
    // public void DoRotateAround(float duration)
    // {
    //     float prevVal = 0.0f;
    //     float endValue;

    //     // durationの時間で値を0～endValueまで変更させて公転処理を呼ぶ
    //     DOTween.To(x => RotateAroundPrc(x), 0.0f, endValue, duration);
    // }

    // /// <summary>
    // /// 公転処理
    // /// </summary>
    // /// <param name="value"></param>
    // private void RotateAroundPrc(float value)
    // {
    //     // 前回との差分を計算
    //     float delta = value - prevVal;
        
    //     // Y軸周りに公転運動
    //     camera.RotateAround(unity_chan.transform.position, Vector3.up, delta);
        
    //     // 前回の角度を更新
    //     prevVal = value;
    // }

    // Update is called once per frame
    void Update()
    {
    }
}
