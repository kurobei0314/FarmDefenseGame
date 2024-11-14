using UnityEngine;
using R3;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerMoveController : MonoBehaviour
    {
        public void Initialize(IPlayerView player, IPlayerEntity playerEntity, ICameraView camera)
        {
            IPlayerMoveUseCase playerUseCase = new PlayerMoveActor(player, playerEntity, camera);
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
                    // TODO: PS4などのゲーム機の場合、歩くが対応できてないため対応する
                    if (Input.GetKey("left shift") || Input.GetKey("right shift")) 
                    {
                        playerUseCase.WalkPlayer(horizontalInput, verticalInput);
                    }
                    else
                    {
                        playerUseCase.RunPlayer(horizontalInput, verticalInput);
                    }
            }).AddTo(this);

            // ボタンから離した時
            moveUpStream.Subscribe(_ => {
                    playerUseCase.StandPlayer();
            }).AddTo(this);
        }
    }
}
