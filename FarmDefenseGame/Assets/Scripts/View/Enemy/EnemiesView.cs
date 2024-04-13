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

        public GameObject GetMinDistanceEnemyFromPlayer()
        {
            var aliveEnemyViews = enemyViews.Where(enemy => enemy.EnemyEntity.CurrentStatus != Status.Die && enemy.IsVisible).ToArray();
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
            return aliveEnemyViews[index].gameObject;
        }

        public GameObject GetNeighborsEnemy(float cameraInput, Transform targetEnemy, Vector3 cameraPositionVector, Vector3 rightCameraVector)
        {
            var aliveEnemyViews = enemyViews.Where(enemy => enemy.EnemyEntity.CurrentStatus != Status.Die 
                                                        && enemy.IsVisible && enemy.GameObject != targetEnemy.gameObject).ToArray();
            if (aliveEnemyViews.Length == 0) return null;
            var index = 0;
            var dot = Vector3.Dot(aliveEnemyViews[0].Position - cameraPositionVector, rightCameraVector);
            for (int i = 1; i < aliveEnemyViews.Length ; i++)
            {
                var enemyDirection = aliveEnemyViews[i].Position - cameraPositionVector;
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
            return aliveEnemyViews[index].GameObject;
        }

        private bool IsInputRightButton(float cameraInput){
            if (cameraInput > 0) return true;
            return false;
        }
    }

}
