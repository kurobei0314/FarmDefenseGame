using UnityEngine;
using WolfVillageBattle.Interface;
using System.Linq;

namespace WolfVillageBattle
{
    public class EnemiesView : MonoBehaviour, IEnemiesView
    {
        [SerializeField] private GameObject enemyPrefab;
        private EnemyView[] enemyViews;

        public void Initialize(InitializeEnemyDTO[] enemyDTOs, IPlayerView playerView, IPlayerEntity playerEntity)
        {
            enemyViews = new EnemyView[enemyDTOs.Length];
            var index = 0;
            foreach(var enemyDTO in enemyDTOs)
            {
                // TODO: Prefabもmasterデータから持ってくる
                var enemyGO = Instantiate(enemyPrefab, enemyDTO.Pos, Quaternion.Euler(enemyDTO.Rotation), this.gameObject.transform);
                var enemyView = enemyGO.GetComponent<EnemyView>();
                if (!enemyView)
                {
                    Debug.LogError("EnemyViewがありません");
                    continue;
                }
                enemyView.Initialize(playerView, enemyDTO.EnemyEntity);
                var enemyAttackPresenter = new EnemyAttackPresenter(enemyView, enemyDTO.EnemyEntity);
                var enemyDamagePresenter = new EnemyDamagePresenter(enemyView, enemyDTO.EnemyEntity, playerEntity);
                // TODO: この書き方も変える
                var presenter = enemyView.GetComponent<EnemyNoticePresenter>();
                if (presenter)
                {
                    presenter.Initialize(enemyView, enemyDTO.EnemyEntity);
                }
                enemyViews[index] = enemyView;
                index++;
            }
        }

        public IEnemyView GetMinDistanceEnemyFromPlayer(ICameraView cameraView)
        {
            var aliveEnemyViews = enemyViews.Where(enemy => enemy.EnemyEntity.CurrentStatus != Status.Die && enemy.IsVisible(cameraView)).ToArray();

            if (aliveEnemyViews.Length == 0) return null;

            var index = 0;
            var minDistance = aliveEnemyViews[0].DistanceFromPlayer();

            for (var i = 1 ; i < aliveEnemyViews.Length; i++)
            {
                var distance = aliveEnemyViews[i].DistanceFromPlayer();
                if (minDistance > distance)
                {
                    index = i;
                    minDistance = distance;
                }
            }
            return aliveEnemyViews[index];
        }

        public IEnemyView GetNeighborsEnemy(float cameraInput, Transform targetEnemy, ICameraView cameraView, Vector3 rightCameraVector)
        {
            var aliveEnemyViews = enemyViews.Where(enemy => enemy.EnemyEntity.CurrentStatus != Status.Die 
                                                        && enemy.IsVisible(cameraView) && enemy.GameObject != targetEnemy.gameObject).ToArray();
            if (aliveEnemyViews.Length == 0) return null;
            var index = -1;
            var dot = Vector3.Dot((targetEnemy.position - cameraView.CameraTrans.position).normalized, rightCameraVector);
            var targetDot = dot;
            var minDistance = 1000000f;
            for (int i = 0; i < aliveEnemyViews.Length ; i++)
            {
                var enemyDirection = aliveEnemyViews[i].Position - cameraView.CameraTrans.position;
                var dotProduct = Vector3.Dot(enemyDirection.normalized, rightCameraVector);
                var distance =  Vector3.Distance(targetEnemy.position, aliveEnemyViews[i].Position);
                if (CheckCondition(cameraInput, dot, dotProduct, distance, minDistance))
                {
                    index = i;
                    dot = dotProduct;
                    minDistance = distance;
                }
            }
            return (index == -1) ? null : aliveEnemyViews[index];
        }

        private bool CheckCondition(float cameraInput, float dot, float indexDotProduct, float distance, float indexDistance)
        {
            if (IsInputRightButton(cameraInput))
            {
                if (dot < indexDotProduct && distance < indexDistance) return true;
                return false;
            }
            else
            {
                if (indexDotProduct < dot && distance < indexDistance) return true;
                return false;
            }
        }

        private bool IsInputRightButton(float cameraInput){
            if (cameraInput < 0) return true;
            return false;
        }
    }
}
