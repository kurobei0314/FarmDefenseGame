using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerMoveController : MonoBehaviour
    {
        private PlayerView player;
        private CameraView camera;

        // Start is called before the first frame update
        public void Initialize(PlayerView player, MainGameRepository mainGameRepository, CameraView camera)
        {
            this.player = player;
            this.camera = camera;

            IPlayerMoveUseCase playerUseCase = new PlayerMoveActor(player, mainGameRepository.Player, camera);
            var moveDownStream = Observable.EveryUpdate()
                                            .Where(_ => Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f ||
                                                        Input.GetKey(KeyCode.UpArrow)    ||  Input.GetKey(KeyCode.DownArrow) ||
                                                        Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow));

            var moveUpStream = Observable.EveryUpdate()
                                            .Where(_ => (Input.GetAxis("Horizontal") == 0.0f && Input.GetAxis("Vertical") == 0.0f) ||
                                                        Input.GetKeyUp(KeyCode.UpArrow)    ||  Input.GetKeyUp(KeyCode.DownArrow) || 
                                                        Input.GetKeyUp(KeyCode.RightArrow) ||  Input.GetKeyUp(KeyCode.LeftArrow));
            
            // プレイヤーの移動系
            moveDownStream.Subscribe(_ => {
                    float horizontalInput = Input.GetAxis("Horizontal");
                    float verticalInput = Input.GetAxis("Vertical");
                    playerUseCase.MovePlayer(horizontalInput, verticalInput);
            }).AddTo(this);

            // ボタンから離した時
            moveUpStream.Subscribe(_ => {
                    playerUseCase.StandPlayer();
            }).AddTo(this);
        }
    }
}
