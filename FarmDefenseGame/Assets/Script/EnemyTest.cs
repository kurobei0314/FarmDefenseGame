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
        DIE,
    }

    private EnemyStatus current_status;
    public EnemyStatus current_enemy_status => current_status;

    // アニメーションが複数回流れないようにするラベル
    // TODO: モデルを作った時に多分なくす
    private int anim_label = 0;

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
    }

    /// <summary>
    /// EnemyStatusをセットする
    /// </summary>
    void SetEnemyStatus(EnemyStatus status)
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
