using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerTest : MonoBehaviour
{
    public enum PlayerStatus
    {
        IDLE,
        ATTACK,
    }

    public PlayerStatus CurrentStatus => current_status;
    private PlayerStatus current_status;
    // Start is called before the first frame update
    void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.UpArrow))
            .Subscribe(_ => {
                Vector3 pos = this.transform.position;
                this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(0.0f, 0.0f, 0.05f));
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.DownArrow))
            .Subscribe(_ => {
                Vector3 pos = this.transform.position;
                this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(0.0f, 0.0f, -0.05f));
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.RightArrow))
            .Subscribe(_ => {
                this.transform.Rotate(0.0f, 0.05f, 0.0f);
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.LeftArrow))
            .Subscribe(_ => {
                this.transform.Rotate(0.0f, -0.05f, 0.0f);
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyUp(KeyCode.UpArrow)    ||  Input.GetKeyUp(KeyCode.DownArrow) || 
                        Input.GetKeyUp(KeyCode.RightArrow) ||  Input.GetKeyUp(KeyCode.LeftArrow))
            .Subscribe(_ => {
                GetComponent<Animator>().Play("Standing(loop)");
        });

        // アタック終わったら、プレイヤーのステータスをIDLEにする
        // TODO: なんかもっといい設計ないのだろうか
        Observable.EveryUpdate()
            .Where(_ => current_status == PlayerStatus.ATTACK)
            .Subscribe(_ => {
                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName("Standing(loop)")){
                    SetPlayerStatus(PlayerStatus.IDLE);
                } 
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.A))
            .Subscribe(_ => {
                SetPlayerStatus(PlayerStatus.ATTACK);
                // MEMO: 攻撃のモーションがなかったのでとりあえずこれで仮置き
                GetComponent<Animator>().Play("KneelDownToUp");
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
