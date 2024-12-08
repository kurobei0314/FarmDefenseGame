using UnityEngine;
using WolfVillage.Battle.Interface;
using UnityEngine.AI;
using System;
using R3;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle
{
    public class EnemyMoveAI : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        // TODO: 今はこう書いてるけど、絶対に動かないのでこれ。。。
        private IPlayerView playerView;
        private IEnemyEntity enemyEntity;
        public bool IsStopped => navMeshAgent.isStopped;

        public void Initialize(IPlayerView playerView, IEnemyEntity enemyEntity)
        {
            this.playerView = playerView;
            this.enemyEntity = enemyEntity;

            Observable.Interval(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_=> {
                    UpdateAI();
                }).AddTo(this);
        }

        private void UpdateAI()
        {
            if (IsStopped) return;
            switch (enemyEntity.CurrentStatus)
            {
                case Status.Idle:
                    SetIdleAI();
                    break;
                case Status.Notice:
                    SetNoticeAI();
                    break;
            }
        }

        private void SetIdleAI()
        {
            StartNavMesh();
            navMeshAgent.destination = playerView.Position + UnityEngine.Random.insideUnitSphere * 5;
        }

        private void SetNoticeAI()
        {
            StartNavMesh();
            navMeshAgent.destination = playerView.Position;
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
