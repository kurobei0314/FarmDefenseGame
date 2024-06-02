using UnityEngine;
using UniRx;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle 
{
    public class PlayerAvoidController : MonoBehaviour
    {
        public void Initialize(IPlayerView playerView, IPlayerEntity playerEntity)
        {
            IPlayerAvoidUseCase playerAvoidUseCase = new PlayerAvoidActor(playerView, playerEntity);
            Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("Avoid"))
                .Subscribe(_ => {
                    playerAvoidUseCase.AvoidEnemy();
                }).AddTo(this);
        }
    }
}
