using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyTest : MonoBehaviour
{
    public enum EnemyStatus
    {
        IDLE,
        NOTICE,
    }

    private EnemyStatus current_enemy_status;

    // Start is called before the first frame update
    void Start()
    {
        this.OnTriggerStayAsObservable()
            .Where(_ => _.gameObject.tag == "Player")
            .Subscribe(_ => {
                SetEnemyStatus(EnemyStatus.NOTICE);
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
        current_enemy_status = status;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
