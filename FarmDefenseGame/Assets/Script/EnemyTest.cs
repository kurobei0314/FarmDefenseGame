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

    // Start is called before the first frame update
    void Start()
    {
        SetEnemyStatus(EnemyStatus.IDLE);

        // 気づいた時の挙動
        this.OnTriggerStayAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Subscribe(_ => {
                SetEnemyStatus(EnemyStatus.NOTICE);
                Debug.Log("wa----------i");
                GetComponent<Animator>().Play("Attack01");
            });
    }

    /// <summary>
    /// 敵が気付いたかどうかを判断する
    /// </summary>
    bool IsNotice()
    {
        return false;
    }

    /// <summary>
    /// EnemyStatusをセットする
    /// </summary>
    void SetEnemyStatus(EnemyStatus status)
    {
        current_status = status;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
