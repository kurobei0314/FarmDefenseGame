
using UnityEngine.InputSystem;
using UnityEngine;

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
        [SerializeField] private PlayerInput playerInput = null;

        void Start()
        {
            mainGameRepository.Initialize();
            var cameraEntity = new CameraEntity();
            var playerEntity = mainGameRepository.Player;
            inGameView.Initialize(playerEntity.CurrentMaxHP, playerEntity.SetCurrentSkills);
            playerView.Initialize(mainGameRepository.FieldVO.PlayerInitPos, mainGameRepository.FieldVO.PlayerInitRot);


            playerStatusView.Initialize(playerEntity);
            playerMoveController.Initialize(playerView, playerEntity, cameraView, playerInput);
            playerAttackController.Initialize(playerView, playerEntity, cameraEntity, enemiesView);
            playerAvoidController.Initialize(playerView, playerEntity, cameraView, playerInput);
            var playerDamagePresenter = new PlayerDamagePresenter(playerView, playerEntity, inGameView, enemiesView);
            playerSkillAttackController.Initialize(playerView, playerEntity, inGameView);

            enemiesView.Initialize(mainGameRepository.Enemies, playerView, playerEntity);
            cameraMoveController.Initialize(playerView, cameraView, cameraEntity, enemiesView, playerInput);
        }
    }
}
