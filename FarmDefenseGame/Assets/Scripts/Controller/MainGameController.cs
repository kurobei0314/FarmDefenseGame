using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;


// TODO:プレイヤーから入力があるのがプレイヤーなら、このクラス名ってControllerなのか、、、？考えていきたい
namespace WolfVillageBattle {
    public class MainGameController : MonoBehaviour
    {
        [SerializeField] private MainGameRepository mainGameRepository;
        [SerializeField] private PlayerMoveController playerMoveController;
        [SerializeField] private PlayerAttackController playerAttackController;
        [SerializeField] private CameraMoveController cameraMoveController;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private CameraView cameraView;
        [SerializeField] private PlayerStatusView playerStatusView;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform enemyParent;

        void Start()
        {
            mainGameRepository.Initialize();

            playerStatusView.Initialize(mainGameRepository);
            playerMoveController.Initialize(playerView, mainGameRepository, cameraView);
            playerAttackController.Initialize(playerView, mainGameRepository);
            var cameraEntity = new CameraEntity();
            cameraMoveController.Initialize(playerView, cameraView, cameraEntity);
            var playerDamagePresenter = new PlayerDamagePresenter(playerView, mainGameRepository.Player);

            foreach(var enemyDTO in mainGameRepository.Enemies)
            {
                // TODO: Prefabもmasterデータから持ってくる
                var enemyGO = Instantiate(enemyPrefab, enemyDTO.Pos, Quaternion.Euler(enemyDTO.Rotation), enemyParent);
                var enemyView = enemyGO.GetComponent<EnemyView>();
                if (!enemyView)
                {
                    Debug.LogError("EnemyViewがありません");
                    continue;
                }
                enemyView.Initialize(playerView, enemyDTO.EnemyEntity);
                var enemyAttackPresenter = new EnemyAttackPresenter(enemyView, enemyDTO.EnemyEntity);
                var enemyDamagePresenter = new EnemyDamagePresenter(enemyView, enemyDTO.EnemyEntity, mainGameRepository.Player);
                // TODO: この書き方も変える
                var presenter = enemyView.GetComponent<EnemyNoticePresenter>();
                if (presenter)
                {
                    presenter.Initialize(enemyView, enemyDTO.EnemyEntity);
                }
            }
        }
    }
}
