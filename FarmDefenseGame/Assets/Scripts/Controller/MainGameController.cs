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
        [SerializeField] private EnemyNoticePresenter[] enemyNoticePresenters;
        [SerializeField] private CameraView cameraView;
        [SerializeField] private PlayerStatusView playerStatusView;

        void Start()
        {
            mainGameRepository.Initialize();

            playerStatusView.Initialize(mainGameRepository);
            playerMoveController.Initialize(playerView, mainGameRepository, cameraView);
            playerAttackController.Initialize(playerView, mainGameRepository);
            cameraMoveController.Initialize(playerView, cameraView);

            for (int i = 0 ; i < enemyNoticePresenters.Length; i++)
            {
                //enemyViews[i].Initialize();
                // TODO: とりあえず、こんな感じで作っているがこれだとエネミーに1種類しか対応できてない
                enemyNoticePresenters[i].Initialize(enemyViews[i], mainGameRepository.Enemies[0]);
            }
        }
    }
}
