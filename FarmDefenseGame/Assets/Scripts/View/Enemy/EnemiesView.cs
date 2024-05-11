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

        public IEnemyView GetNeighborsEnemy(float cameraInput, Transform targetEnemy, ICameraView cameraView)
        {
            var aliveEnemyViews = enemyViews.Where(enemy => enemy.EnemyEntity.CurrentStatus != Status.Die 
                                                        && enemy.IsVisible(cameraView) && enemy.GameObject != targetEnemy.gameObject).ToArray();
            if (aliveEnemyViews.Length == 0) return null;
            var index = 0;
            var dot = CalculateDotProduction(targetEnemy.position, aliveEnemyViews[0].transform.position, cameraView.CameraTrans);
            for (int i = 1; i < aliveEnemyViews.Length ; i++)
            {
                var indexDotProduct = CalculateDotProduction(targetEnemy.position, aliveEnemyViews[i].transform.position, cameraView.CameraTrans);
                if (CheckCondition(cameraInput, dot, indexDotProduct))
                {
                    index = i;
                    dot = indexDotProduct;
                }
            }
            if (IsInputRightButton(cameraInput)) return dot > 0 ? aliveEnemyViews[index] : null;
            else                                 return dot < 0 ? aliveEnemyViews[index] : null;
        }

        private float CalculateDotProduction(Vector3 targetEnemyPosition, Vector3 aliveEnemyPosition, Transform CameraTrans)
        {
            var ab = targetEnemyPosition - CameraTrans.position;
            var acRight = Vector3.Cross(aliveEnemyPosition - CameraTrans.position, CameraTrans.up);
            return Vector3.Dot(new Vector3(ab.x, 0, ab.z).normalized, new Vector3(acRight.x, 0, acRight.z).normalized);
        }

        private bool CheckCondition(float cameraInput, float dot, float indexDotProduct)
        {
            if (IsInputRightButton(cameraInput))
            {
                if (dot < 0) return true;
                if (indexDotProduct < dot && indexDotProduct > 0) return true;
                return false;
            }
            else
            {
                if (dot > 0) return true;
                if (dot < indexDotProduct && indexDotProduct < 0) return true;
                return false;
            }
        }

        private bool IsInputRightButton(float cameraInput){
            if (cameraInput < 0) return true;
            return false;
        }
    }
}
