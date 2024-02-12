using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;
using System;
using UniRx;
using System.Data.Common;
using UnityEditor;
using UniRx.Triggers;

namespace WolfVillageBattle
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        [SerializeField] private EnemyMoveAI enemyMoveAI;
        [SerializeField] private EnemyStatusView enemyStatusView;
        [SerializeField] private EnemyAnimationView enemyAnimationView;
        [SerializeField] private EnemySEView enemySEView;
        [SerializeField] private HPBarView enemyHpBarView;
        [SerializeField] private GameObject body;

        public GameObject GameObject => this.gameObject;
        public Vector3 Position => this.gameObject.transform.position;
        public Rigidbody Rigidbody => this.GetComponent<Rigidbody>();
        public GameObject Body => body;
        private PlayerView playerView;

        public IEnemyEntity EnemyEntity => enemyEntity;
        private IEnemyEntity enemyEntity;

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
            enemyAnimationView.Attack();
        }

        public void Damage()
        {
            enemyMoveAI.StopNavMesh();
            enemyAnimationView.PlayDamageAnim();
            enemySEView.PlayDamageSound();
            enemyHpBarView.SetValue((float)enemyEntity.CurrentHPValue/enemyEntity.EnemyVO.MaxHP);
        }

        public void Die()
        {
            enemyMoveAI.StopNavMesh();
            enemyAnimationView.PlayDieAnim();
            enemySEView.PlayDieSound();
            enemyHpBarView.SetValue((float)enemyEntity.CurrentHPValue/enemyEntity.EnemyVO.MaxHP);
        }

        private float DistanceFromPlayer()
        {
            return Vector3.Distance(playerView.Position, Position);
        }
    }
}