using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

// TODO：VO的な立ち位置にしたい
public class EnemyTest : MonoBehaviour
{
    public enum EnemyStatus
    {
        IDLE,
        NOTICE,
        ATTACK,
        ANIMATION, // なんかとりあえず、気づいた時とか見失った時アニメーションをしたい時にやる(もっといい方法ある絶対)
        DAMAGE,
        DIE,
    }

    // TODO: 命名規則もしっかりしよーねー
    private EnemyStatus current_status;
    public EnemyStatus current_enemy_status => current_status;

    // アニメーションが複数回流れないようにするラベル
    // TODO: モデルを作った時に多分なくす
    private int anim_label = 0;

    // 敵の攻撃力
    private int attack = 3;
    public int Attack => attack;

    // 敵のHP
    private int hp = 2;
    public int HP => hp;

    // MEMO: とりあえずスライムのゲームオブジェクトを持ってくる
    // 自分でモデリング作るときはこういう作りにしたくないな。。なっちゃうのかな。
    [SerializeField]
    GameObject Body;

    // 敵が死んだ時のObservable(ゲームダンジョン用)
    private Subject<int> enemyDie = new Subject<int>();
    public IObservable<int> EnemyDieObservable => enemyDie;

    // Start is called before the first frame update
    void Start()
    {
        SetEnemyStatus(EnemyStatus.IDLE);

        // 気づいた時の挙動
        this.OnTriggerEnterAsObservable()
            .Where(_ => _.gameObject.tag == "Player" && current_status == EnemyStatus.IDLE && anim_label == 0)
            .Subscribe(_ => {
                Debug.Log("気づいちゃったんだおー");
                StartCoroutine ("ChangeNoticeFromIdle");
        }).AddTo(this);

        // 見逃した時の挙動
        this.OnTriggerExitAsObservable()
            .Where(_ => _.gameObject.tag == "Player" && current_status == EnemyStatus.NOTICE && anim_label == 0)
            .Subscribe(_ => {
                Debug.Log("見逃したー");
                StartCoroutine ("ChangeIdleFromNotice");
            }).AddTo(this);

        // プレイヤーが攻撃した時の挙動(Enterにしたいけど、今のcolliderの設定的にstayの方がいいかもしれない)
        Body.OnTriggerStayAsObservable()
            .Where(_ =>  _.gameObject.tag == "PlayerWeapon")
            .Subscribe(_ => {
                var root_gameObject = _.gameObject.transform.root.gameObject;
                if (root_gameObject.GetComponent<PlayerTest>().CurrentStatus != PlayerTest.PlayerStatus.ATTACK) return;
                if (current_enemy_status == EnemyStatus.DAMAGE || current_enemy_status == EnemyStatus.DIE) return;
                Debug.Log("今のcurrent_enemy_status:"+ current_enemy_status);
                Debug.Log("攻撃したおー");
                Debug.Log("current_enemy_status:"+ current_enemy_status);
                ReserveDamage(root_gameObject.GetComponent<PlayerTest>().Attack);
                if (hp == 0){
                    Debug.Log("倒れたー");
                    SetEnemyStatus(EnemyStatus.DIE);
                    AudioManager.Instance.PlaySE("slime_die");
                    GetComponent<Animator>().Play("Die");
                    enemyDie.OnNext(1);
                }
                else {
                    Debug.Log("ダメージ受けたー");
                    GetComponent<Animator>().Play("GetHit");
                    SetEnemyStatus(EnemyStatus.DAMAGE);
                }
        }).AddTo(this);

        // アタック終わったら、プレイヤーのステータスをIDLEにする
        // TODO: なんかもっといい設計ないのだろうか
        Observable.EveryUpdate()
            .Where(_ => current_enemy_status == EnemyStatus.DAMAGE || current_enemy_status == EnemyStatus.DIE)
            .Subscribe(_ => {
                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName("IdleNormal")){
                    if      (current_enemy_status == EnemyStatus.DAMAGE){
                        Debug.Log("NOTICEに戻すよー");
                        SetEnemyStatus(EnemyStatus.NOTICE);
                    }
                    else if (current_enemy_status == EnemyStatus.DIE){
                        this.gameObject.SetActive(false);
                    }
                } 
        }).AddTo(this);

        // アタック終わったら、プレイヤーのステータスをIDLEにする
        // TODO: なんかもっといい設計ないのだろうか
        // Observable.EveryUpdate()
        //     .Where(_ => current_enemy_status == EnemyStatus.DIE)
        //     .Subscribe(_ => {
        //         if (GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName("Dizzy")){
        //             SetEnemyStatus(EnemyStatus.NOTICE);
        //         } 
        // }).AddTo(this);

    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.tag != "Player") return;
    //     if (collision.gameObject.GetComponent<PlayerTest>().CurrentStatus != PlayerTest.PlayerStatus.ATTACK) return;
    //     SetEnemyStatus(EnemyStatus.DAMAGE);
    //     GetComponent<Animator>().Play("GetHit");
    // }

    // void OnTriggerStay(Collider collision)
    // {
    //     if (collision.gameObject.tag != "Player") return;
    //     if (collision.gameObject.GetComponent<PlayerTest>().CurrentStatus != PlayerTest.PlayerStatus.ATTACK) return;
    //     SetEnemyStatus(EnemyStatus.DAMAGE);
    //     GetComponent<Animator>().Play("GetHit");
    // }
    
    /// <summary>
    /// EnemyStatusをセットする
    /// </summary>
    public void SetEnemyStatus(EnemyStatus status)
    {
        current_status = status;
    }

    /// <summary>
    /// idleからnoticeにステータスに変更する
    /// TODO: 絶対にモデルを変更した時にこれはなくなる
    /// </summary>
    private IEnumerator ChangeNoticeFromIdle()
    {
        anim_label = 1;
        SetEnemyStatus(EnemyStatus.ANIMATION);
        AudioManager.Instance.PlaySE("slime_found");
        GetComponent<Animator>().Play("Victory");
        yield return new WaitForSeconds(4.0f);
        // 攻撃とか受けてステータスが変わってないことを見る
        if (current_enemy_status == EnemyStatus.ANIMATION) SetEnemyStatus(EnemyStatus.NOTICE);
        anim_label = 0;
    }

    /// <summary>
    /// noticeからidleにステータスに変更する
    /// TODO: 絶対にモデルを変更した時にこれはなくなる
    /// </summary>
    private IEnumerator ChangeIdleFromNotice()
    {
        anim_label = 1;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        SetEnemyStatus(EnemyStatus.ANIMATION);
        GetComponent<Animator>().Play("SenseSomethingRPT");
        yield return new WaitForSeconds(5.0f);
        // 攻撃とか受けてステータスが変わってないことを見る
        if (current_enemy_status == EnemyStatus.ANIMATION) SetEnemyStatus(EnemyStatus.IDLE);
        anim_label = 0;
    }

    /// <summary>
    /// HPを減少させる
    /// TODO: playerと共通化絶対にさせる
    /// </summary> 
    private void ReserveDamage(int value){
        if( hp - value < 0 ){
            hp = 0;
        }
        else{
            hp -= value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
