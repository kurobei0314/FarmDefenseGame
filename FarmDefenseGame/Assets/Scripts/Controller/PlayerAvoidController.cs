using UnityEngine;
using R3;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle 
{
    public class PlayerAvoidController : MonoBehaviour
    {
        public void Initialize(IPlayerView playerView, IPlayerEntity playerEntity, ICameraView cameraView)
        {
            IPlayerAvoidUseCase playerAvoidUseCase = new PlayerAvoidActor(playerView, playerEntity, cameraView);
            Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("Avoid"))
                .Subscribe(_ => {
                    float horizontalInput = Input.GetAxis("Horizontal");
                    float verticalInput = Input.GetAxis("Vertical");
                    playerAvoidUseCase.AvoidEnemy(horizontalInput, verticalInput);
                }).AddTo(this);
        }
    }
}
