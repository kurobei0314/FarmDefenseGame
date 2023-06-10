using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


// TODO：VO的な立ち位置にしたい
public class EnemyTest : MonoBehaviour
{
    public enum EnemyStatus
    {
        IDLE,
        NOTICE,
        ATTACK,
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

    // Start is called before the first frame update
    void Start()
    {
        SetEnemyStatus(EnemyStatus.IDLE);

        // 気づいた時の挙動
        this.OnTriggerStayAsObservable()
            .Where(_ => _.gameObject.tag == "Player" && current_status == EnemyStatus.IDLE && anim_label == 0)
            .Subscribe(_ => {
                StartCoroutine ("ChangeNoticeFromIdle");
        }).AddTo(this);

        // プレイヤーが攻撃した時の挙動
        Body.OnTriggerEnterAsObservable()
            .Where(_ => _.gameObject.tag == "Player" && _.gameObject.GetComponent<PlayerTest>().CurrentStatus == PlayerTest.PlayerStatus.ATTACK)
            .Subscribe(_ => {
                if (current_enemy_status == EnemyStatus.DAMAGE || current_enemy_status == EnemyStatus.DIE) return;
                Debug.Log("攻撃したおー");
                ReserveDamage(_.gameObject.GetComponent<PlayerTest>().Attack);
                if (hp == 0){
                    SetEnemyStatus(EnemyStatus.DIE);
                    GetComponent<Animator>().Play("Die");
                }
                else {
                    SetEnemyStatus(EnemyStatus.DAMAGE);
                    GetComponent<Animator>().Play("GetHit");
                }
        }).AddTo(this);

        // アタック終わったら、プレイヤーのステータスをIDLEにする
        // TODO: なんかもっといい設計ないのだろうか
        Observable.EveryUpdate()
            .Where(_ => current_enemy_status == EnemyStatus.DAMAGE)
            .Subscribe(_ => {
                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName("Dizzy")){
                    SetEnemyStatus(EnemyStatus.NOTICE);
                } 
        }).AddTo(this);

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
        SetEnemyStatus(EnemyStatus.NOTICE);
        GetComponent<Animator>().Play("Victory");
        yield return new WaitForSeconds(3.0f);
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
