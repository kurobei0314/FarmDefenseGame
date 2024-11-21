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
        [SerializeField] private PlayerNormalAttackController playerAttackController;
        [SerializeField] private PlayerAvoidController playerAvoidController;
        [SerializeField] private CameraMoveController cameraMoveController;
        [SerializeField] private PlayerSkillAttackController playerSkillAttackController;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private CameraView cameraView;
        [SerializeField] private PlayerStatusView playerStatusView;
        [SerializeField] private EnemiesView enemiesView;
        [SerializeField] private InGameView inGameView;

        void Start()
        {
            mainGameRepository.Initialize();
            var cameraEntity = new CameraEntity();
            var playerEntity = mainGameRepository.Player;
            inGameView.Initialize(playerEntity.CurrentMaxHP, playerEntity.SetCurrentSkills);

            playerStatusView.Initialize(playerEntity);
            playerMoveController.Initialize(playerView, playerEntity, cameraView);
            playerAttackController.Initialize(playerView, playerEntity, cameraEntity, enemiesView);
            playerAvoidController.Initialize(playerView, playerEntity, cameraView);
            var playerDamagePresenter = new PlayerDamagePresenter(playerView, playerEntity, inGameView, enemiesView);
            playerSkillAttackController.Initialize(playerView, playerEntity, inGameView);

            enemiesView.Initialize(mainGameRepository.Enemies, playerView, playerEntity);
            cameraMoveController.Initialize(playerView, cameraView, cameraEntity, enemiesView);
        }
    }
}
