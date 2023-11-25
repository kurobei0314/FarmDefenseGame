using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;
using UnityEngine.AI;
using System;
using UniRx;

namespace WolfVillageBattle
{
    public class EnemyMoveAI : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        // TODO: 今はこう書いてるけど、絶対に動かないのでこれ。。。
        private PlayerView playerView;
        private EnemyView enemyView;
        private EnemyEntity enemyEntity;
        public bool IsStopped => navMeshAgent.isStopped;

        public void Initialize(IPlayerView playerView, IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            this.playerView = (PlayerView)playerView;
            this.enemyView = (EnemyView) enemyView;
            this.enemyEntity = (EnemyEntity) enemyEntity;

            Observable.Timer(TimeSpan.FromSeconds(1.0f))
                .Subscribe(_=> {
                    UpdateAI();
                }).AddTo(this);
        }

        private void UpdateAI()
        {
            if (IsStopped) return;
            switch (this.enemyEntity.CurrentStatus)
            {
                case Status.IDLE:
                    SetIdleAI();
                    break;
                case Status.NOTICE:
                    SetNoticeAI();
                    break;
            }
        }

        public void SetIdleAI()
        {
            StartNavMesh();
            navMeshAgent.destination = playerView.Position + UnityEngine.Random.insideUnitSphere * 5;
            enemyView.PlayWalk();
        }

        public void SetNoticeAI()
        {
            StartNavMesh();
            navMeshAgent.destination = playerView.Position;
            enemyView.PlayWalk();
        }

        public void StopNavMesh()
        {
            navMeshAgent.isStopped = true;
        }

        public void StartNavMesh()
        {
            navMeshAgent.isStopped = false;
        }
    }
}
