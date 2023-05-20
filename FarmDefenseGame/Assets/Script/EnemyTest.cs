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
        });

        // プレイヤーが攻撃した時の挙動
        Body.OnTriggerEnterAsObservable()
            .Where(_ => _.gameObject.tag == "Player" && _.gameObject.GetComponent<PlayerTest>().CurrentStatus == PlayerTest.PlayerStatus.ATTACK)
            .Subscribe(_ => {
                if (current_enemy_status == EnemyStatus.DAMAGE || current_enemy_status == EnemyStatus.DIE) return;
                SetEnemyStatus(EnemyStatus.DAMAGE);
                Debug.Log("攻撃されたおー");
                GetComponent<Animator>().Play("GetHit");
        });
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
