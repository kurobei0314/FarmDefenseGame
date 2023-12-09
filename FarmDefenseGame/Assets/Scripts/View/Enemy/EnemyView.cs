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
        [SerializeField] EnemyMoveAI enemyMoveAI;
        [SerializeField] EnemyStatusView enemyStatusView;
        [SerializeField] EnemyAnimationView enemyAnimationView;
        [SerializeField] EnemySEView enemySEView;
        

        public GameObject GameObject => this.gameObject;
        public Vector3 Position => this.gameObject.transform.position;
        public Rigidbody Rigidbody => this.GetComponent<Rigidbody>();
        public GameObject Body => throw new System.NotImplementedException();
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
                    JudgeAttackable();
                }).AddTo(this);
        }

        // TODO: ここよくないので直す
        private void JudgeAttackable()
        {
            if (enemyEntity.CurrentStatus == Status.NOTICE)
            {
                if ( DistanceFromPlayer() < 2.0f )
                {
                    StartAttack();
                } 
            }
            else if (enemyEntity.CurrentStatus == Status.ATTACK)
            {
                if ( DistanceFromPlayer() >= 2.0f )
                {
                    StopAttack();
                }
            }
        }

        private void StartAttack()
        {
            enemyMoveAI.StopNavMesh();
            startAttackSubject.OnNext(Unit.Default);
        }

        private void StopAttack()
        {
            enemyMoveAI.StartNavMesh();
            stopAttackSubject.OnNext(Unit.Default);
        }

        public void Notice()
        {
            enemyMoveAI.StopNavMesh();
            enemySEView.PlayNoticeSound();
            enemyAnimationView.PlayNoticeAnim();
        }

        public void Outlook()
        {
            enemyMoveAI.StopNavMesh();
            enemyAnimationView.PlayOverlookAnim();
        }

        public void Attack()
        {
            enemyMoveAI.StopNavMesh();
            enemyAnimationView.PlayAttackAnim();
        }

        private float DistanceFromPlayer()
        {
            return Vector3.Distance(playerView.Position, Position);
        }
    }
}