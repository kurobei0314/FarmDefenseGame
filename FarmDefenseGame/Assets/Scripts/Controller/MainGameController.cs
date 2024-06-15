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
        [SerializeField] private PlayerAvoidController playerAvoidController;
        [SerializeField] private CameraMoveController cameraMoveController;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private CameraView cameraView;
        [SerializeField] private PlayerStatusView playerStatusView;
        [SerializeField] private EnemiesView enemiesView;
        [SerializeField] private InGameView inGameView;

        void Start()
        {
            mainGameRepository.Initialize();
            var cameraEntity = new CameraEntity();
            inGameView.Initialize(mainGameRepository.Player.PlayerVO.MaxHP);

            playerStatusView.Initialize(mainGameRepository);
            playerMoveController.Initialize(playerView, mainGameRepository, cameraView);
            playerAttackController.Initialize(playerView, mainGameRepository, cameraEntity, enemiesView);
            playerAvoidController.Initialize(playerView, mainGameRepository.Player, cameraView);
            var playerDamagePresenter = new PlayerDamagePresenter(playerView, mainGameRepository.Player, inGameView, enemiesView);

            enemiesView.Initialize(mainGameRepository.Enemies, playerView, mainGameRepository.Player);
            cameraMoveController.Initialize(playerView, cameraView, cameraEntity, enemiesView);
        }
    }
}
