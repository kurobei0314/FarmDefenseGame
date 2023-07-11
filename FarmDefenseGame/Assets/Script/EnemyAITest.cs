using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class EnemyAITest : MonoBehaviour
{
    // TODO: ここで変数持たせるのを絶対にやめる
    [SerializeField]
    private PlayerTest player;

    public enum EnemyDirectionMovement
    {
        FRONT,
        BACK,
        RIGHT,
        LEFT,
        STAY,
    }

    private EnemyDirectionMovement current_direction_movement;
    private float current_amount_distance = 0.0f;
    // １回攻撃した後に攻撃するまでの時間
    private float attack_time = 1f;
    private float current_attack_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;

        // 気づいていない時の行動
        Observable.EveryUpdate()
            .Where(_ => this.gameObject.GetComponent<EnemyTest>().current_enemy_status == EnemyTest.EnemyStatus.IDLE)
            .Subscribe(_ => {
                if (player.CurrentStatus == PlayerTest.PlayerStatus.DIE) return;
                // Debug.Log("わーーーーーーーーーーい");
                // this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
                IdleAI();
            }).AddTo(this);
        
        // 気づいた時の行動
        Observable.EveryUpdate()
            .Where(_ => this.gameObject.GetComponent<EnemyTest>().current_enemy_status == EnemyTest.EnemyStatus.NOTICE
                    ||  this.gameObject.GetComponent<EnemyTest>().current_enemy_status == EnemyTest.EnemyStatus.ATTACK)
            .Subscribe(_ => {
                if (player.CurrentStatus == PlayerTest.PlayerStatus.DIE) return;
                // Debug.Log();
                NoticeAI();
            }).AddTo(this);
    }

    // 気づいてない時の動き 
    // TODO: 敵によって変化すると思うので、敵の種類によって変化できるように設計を考える
    // MEMO: 今回はランダムに動くようにする。
    void IdleAI()
    {
        // 移動しきったら初期化する
        if (current_amount_distance <= 0.0f)
        {
            InitializeMovementDistance();
        }

        Vector3 pos = this.transform.position;
        GetComponent<Animator>().Play("WalkFWD");
        current_amount_distance -= 0.01f;
        this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(0.0f, 0.0f, 0.01f));
    }

    // この移動で動く移動距離を指定する(初期化)
    void InitializeMovementDistance()
    {
        int num = Enum.GetNames(typeof(EnemyDirectionMovement)).Length;
        current_direction_movement = (EnemyDirectionMovement)UnityEngine.Random.Range(0, num);
        current_amount_distance = UnityEngine.Random.Range(5.0f, 10.0f);
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        GetComponent<Rigidbody>().MoveRotation(deltaRotation);

        switch ((EnemyDirectionMovement)current_direction_movement)
        {
            case EnemyDirectionMovement.FRONT:
                break;
            case EnemyDirectionMovement.BACK:
                deltaRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                GetComponent<Rigidbody>().MoveRotation(deltaRotation);
                break;
            case EnemyDirectionMovement.RIGHT:
                deltaRotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
                GetComponent<Rigidbody>().MoveRotation(deltaRotation);
                break;
            case EnemyDirectionMovement.LEFT:
                deltaRotation = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f));
                GetComponent<Rigidbody>().MoveRotation(deltaRotation);
                Debug.Log(current_direction_movement);
                break;
            case EnemyDirectionMovement.STAY:
                break;
            default:
                Debug.Log(current_direction_movement);
                break;
        }
    }

    // 気づいている時の動き
    // TODO: 敵によって変化すると思うので、敵の種類によって変化できるように設計を考える
    // MEMO: 今回は離れてたらプレイヤーに近づいて、ある程度近づいたら攻撃するようにする
    void NoticeAI()
    {
        // Debug.Log("気づいたー");

        // 距離が近かったら
        if (DistanceFromPlayer() < 2.0f) 
        {
            // TODO: 書き方マージで良くない
            this.gameObject.GetComponent<EnemyTest>().SetEnemyStatus(EnemyTest.EnemyStatus.ATTACK);
            AttackPlayer();
        }
        else
        {
            // TODO: 書き方マージで良くない
            this.gameObject.GetComponent<EnemyTest>().SetEnemyStatus(EnemyTest.EnemyStatus.NOTICE);
            MoveToPlayer();
        }

    }

    // 敵とプレイヤーの距離を求める
    float DistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, this.transform.position);
    }

    // プレイヤーへ向かって動く
    void MoveToPlayer()
    {
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = player.transform.position;
        GetComponent<Animator>().Play("WalkFWD");
    }

    // プレイヤーへ攻撃する
    void AttackPlayer()
    {
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        // TODO:この書き方も良くないので修正したい
        if (this.gameObject.GetComponent<EnemyTest>().current_enemy_status != EnemyTest.EnemyStatus.ATTACK) return;
        // MEMO: なんかアタックする時にプレイヤーの声が消えちゃう
        // AudioManager.Instance.PlaySE("slime_attack");
        GetComponent<Animator>().Play("Attack02");
    }
}
