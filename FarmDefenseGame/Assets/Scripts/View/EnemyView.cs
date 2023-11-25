using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;
using System;
using UniRx;
using System.Data.Common;
using UnityEditor;

namespace WolfVillageBattle
{
    public class EnemyView : MonoBehaviour, IEnemyView
{

    [SerializeField] private Animator animator;
    [SerializeField] EnemyMoveAI enemyMoveAI;

    public GameObject GameObject => this.gameObject;
    public Vector3 Position => this.gameObject.transform.position;
    public Rigidbody Rigidbody => this.GetComponent<Rigidbody>();
    public GameObject Body => throw new System.NotImplementedException();
    public Animator Animator => animator;
    private PlayerView playerView;
    private EnemyEntity enemyEntity;

    private Subject<Unit> startAttackSubject = new Subject<Unit>();
    public IObservable<Unit> StartAttackObservable => startAttackSubject;

    private Subject<Unit> stopAttackSubject = new Subject<Unit>();
    public IObservable<Unit> StopAttackObservable => stopAttackSubject;

    // TODO: 絶対にコンストラクタにする
    public void Initialize (IPlayerView playerView, IEnemyEntity enemyEntity)
    {
        this.playerView = (PlayerView)playerView;
        this.enemyEntity = (EnemyEntity) enemyEntity;

        Observable.EveryUpdate()
            .Subscribe(_ => {
                if (enemyEntity.CurrentStatus == Status.NOTICE)
                {
                    if ( DistanceFromPlayer() < 2.0f ){
                        enemyMoveAI.StopNavMesh();
                        startAttackSubject.OnNext(Unit.Default);
                    } 
                }
                if (enemyEntity.CurrentStatus == Status.ATTACK)
                {
                    if ( DistanceFromPlayer() >= 2.0f ){
                        enemyMoveAI.StartNavMesh();
                        stopAttackSubject.OnNext(Unit.Default);
                    }
                }
            }).AddTo(this);
        
    }

    // 敵とプレイヤーの距離を求める
    private float DistanceFromPlayer()
    {
        return Vector3.Distance(playerView.Position, Position);
    }

    public void PlayWalk()
    {
        animator.Play("WalkFWD");
    }

    public void PlayStand()
    {
        throw new System.NotImplementedException();
    }

    public void PlayAttack()
    {
        throw new System.NotImplementedException();
    }

    public void PlayAttackSound()
    {
        throw new System.NotImplementedException();
    }

    public void PlayNotice()
    {
        animator.Play("Victory");
    }

    public void PlayOverlook()
    {
        animator.Play("SenseSomethingRPT");
    }

    public void PlayNoticeSound()
    {
        AudioManager.Instance.PlaySE("slime_found");
    }

    public void PlayOverlookSound()
    {
        throw new System.NotImplementedException();
    }
}

}