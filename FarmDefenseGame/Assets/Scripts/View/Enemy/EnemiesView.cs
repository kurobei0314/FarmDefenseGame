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
            var index = 0;
            var dot = Vector3.Dot(aliveEnemyViews[0].Position - cameraView.CameraTrans.position, rightCameraVector);
            for (int i = 1; i < aliveEnemyViews.Length ; i++)
            {
                var enemyDirection = aliveEnemyViews[i].Position - cameraView.CameraTrans.position;
                var dotProduct = Vector3.Dot(enemyDirection.normalized, rightCameraVector);
                if (IsInputRightButton(cameraInput))
                {
                    if (dotProduct < dot && dotProduct > 0)
                    {
                        index = i;
                        dot = dotProduct;
                    }
                }
                else
                {
                    if (dot < dotProduct && dotProduct < 0)
                    {
                        index = i;
                        dot = dotProduct;
                    }
                }
            }
            Debug.LogError("index: " + index);
            return aliveEnemyViews[index];
        }

        private bool IsInputRightButton(float cameraInput){
            if (cameraInput < 0) return true;
            return false;
        }
    }

}
