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
        [SerializeField] private EnemyView[] enemyViews;
        // [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private EnemyNoticePresenter[] enemyNoticePresenters;
        [SerializeField] private EnemyStatusView[] enemyStatusViews;
        [SerializeField] private EnemyMoveAI[] enemyMoveAIs;
        [SerializeField] private CameraView cameraView;
        [SerializeField] private PlayerStatusView playerStatusView;

        // private GameObject[] enemyPrefabs; 

        void Start()
        {
            mainGameRepository.Initialize();

            playerStatusView.Initialize(mainGameRepository);
            playerMoveController.Initialize(playerView, mainGameRepository, cameraView);
            playerAttackController.Initialize(playerView, mainGameRepository);
            cameraMoveController.Initialize(playerView, cameraView);

            // enemyPrefabs = new GameObject[enemyNoticePresenters.Length];

            for (int i = 0 ; i < enemyNoticePresenters.Length; i++)
            {
                // GameObject enemy = Instantiate(enemyPrefab, new Vector3(5.81f, 0.0f, 0.0f), Quaternion.identity);
                // enemyPrefabs[i] = enemy;
                // var enemyView = new EnemyView(playerView, mainGameRepository.Enemies[0]);
                enemyViews[i].Initialize(playerView, mainGameRepository.Enemies[0]);
                // TODO: とりあえず、こんな感じで作っているがこれだとエネミーに1種類しか対応できてない
                var enemyAttackPresenter = new EnemyAttackPresenter(enemyViews[i], mainGameRepository.Enemies[0]);
                enemyNoticePresenters[i].Initialize(enemyViews[i], mainGameRepository.Enemies[0]);
                enemyStatusViews[i].Initialize(mainGameRepository.Enemies[0]);
                enemyMoveAIs[i].Initialize(playerView, enemyViews[i], mainGameRepository.Enemies[0]);
            }
        }
    }
}
