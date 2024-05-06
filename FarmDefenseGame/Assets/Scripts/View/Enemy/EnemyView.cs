using UnityEngine;
using WolfVillageBattle.Interface;
using System;
using UniRx;

namespace WolfVillageBattle
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        [SerializeField] private EnemyMoveAI enemyMoveAI;
        [SerializeField] private EnemyStatusView enemyStatusView;
        [SerializeField] private EnemyAnimationView enemyAnimationView;
        [SerializeField] private EnemySEView enemySEView;
        [SerializeField] private EnemyCanvasView enemyCanvasView;
        [SerializeField] private GameObject body;
        [SerializeField] private Renderer targetRenderer;

        public GameObject GameObject => this.gameObject;
        public Vector3 Position => this.gameObject.transform.position;
        public Rigidbody Rigidbody => this.GetComponent<Rigidbody>();
        public GameObject Body => body;
        private PlayerView playerView;
        public Boolean IsVisible(ICameraView cameraView)
        {
            if (!cameraView.IsVisibleInCamera(Position)) return false;
            var direction = Position - cameraView.CameraTrans.position;
            var ray = new Ray(cameraView.CameraTrans.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.CompareTag("Enemy")) return true;
            }
            return false;
        }

        private Boolean IsCameraVisible(Vector3 viewPos)
        {
            return (viewPos.x >= 0 && viewPos.x <=1 && viewPos.y >= 0 && viewPos.y <=1 && viewPos.z >=0) ? true : false;
        }

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
            enemyCanvasView.Initialize();

            Observable.EveryUpdate()
                .Subscribe(_ => {
                    JudgeAttackable();
                }).AddTo(this);
        }

        // TODO: ここよくないので直す
        private void JudgeAttackable()
        {
            if (enemyEntity.CurrentStatus == Status.Notice)
            {
                if ( DistanceFromPlayer() < 2.0f )
                {
                    StartAttack();
                } 
            }
            else if (enemyEntity.CurrentStatus == Status.Attack)
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
            enemyCanvasView.SetHPBarValue((float)enemyEntity.CurrentHPValue/enemyEntity.EnemyVO.MaxHP);
        }

        public void Die()
        {
            enemyMoveAI.StopNavMesh();
            enemyAnimationView.PlayDieAnim();
            enemySEView.PlayDieSound();
            enemyCanvasView.SetHPBarValue((float)enemyEntity.CurrentHPValue/enemyEntity.EnemyVO.MaxHP);
        }

        public float DistanceFromPlayer()
        {
            return Vector3.Distance(playerView.Position, Position);
        }

        public void SetTargetLockActive(bool active)
        {
            enemyCanvasView.SetTargetLockActive(active);
        }
    }
}