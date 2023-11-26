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
    [SerializeField] EnemyStatusView enemyStatusView;

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
        this.playerView = (PlayerView) playerView;
        this.enemyEntity = (EnemyEntity) enemyEntity;
        enemyMoveAI.Initialize(playerView, enemyEntity);
        enemyStatusView.Initialize(enemyMoveAI, enemyEntity);

        Observable.EveryUpdate()
            .Subscribe(_ => {
                if (enemyEntity.CurrentStatus == Status.NOTICE)
                {
                    if ( DistanceFromPlayer() < 2.0f )
                    {
                        enemyMoveAI.StopNavMesh();
                        startAttackSubject.OnNext(Unit.Default);
                    } 
                }
                else if (enemyEntity.CurrentStatus == Status.ATTACK)
                {
                    if ( DistanceFromPlayer() >= 2.0f )
                    {
                        enemyMoveAI.StartNavMesh();
                        stopAttackSubject.OnNext(Unit.Default);
                    }
                }
            }).AddTo(this);
    }

    public void PlayNotice()
    {
        enemyMoveAI.StopNavMesh();
        PlayNoticeSound();
        PlayNoticeAnim();
    }

    // 敵とプレイヤーの距離を求める
    private float DistanceFromPlayer()
    {
        return Vector3.Distance(playerView.Position, Position);
    }

    public void PlayWalkAnim()
    {
        animator.Play("WalkFWD");
    }

    public void PlayStandAnim()
    {
        throw new System.NotImplementedException();
    }

    public void PlayAttackAnim()
    {
        animator.Play("Attack02");
    }

    public void PlayAttackSound()
    {
        throw new System.NotImplementedException();
    }

    public void PlayNoticeAnim()
    {
        animator.Play("Victory");
    }

    public void PlayOverlookAnim()
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