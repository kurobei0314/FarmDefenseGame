using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

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
    }

}
