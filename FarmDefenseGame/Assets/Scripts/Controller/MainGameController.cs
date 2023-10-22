using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;


// TODO:プレイヤーから入力があるのがプレイヤーなら、このクラス名ってControllerなのか、、、？考えていきたい
namespace WolfVillageBattle {
    public class MainGameController : MonoBehaviour
    {
        [SerializeField]
        private MainGameRepository mainGameRepository;
        
        [SerializeField]
        private PlayerMoveController playerMoveController;
        
        [SerializeField]
        private PlayerAttackController playerAttackController;

        [SerializeField]
        private PlayerView playerView;

        [SerializeField]
        private CameraView cameraView;

        [SerializeField]
        private PlayerStatusView playerStatusView;

        // Start is called before the first frame update
        void Start()
        {
            mainGameRepository.Initialize();
            playerStatusView.Initialize(mainGameRepository);
            playerMoveController.Initialize(playerView, mainGameRepository, cameraView);
            playerAttackController.Initialize(playerView, mainGameRepository);
        }
    }
}
