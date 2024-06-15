using UnityEngine;
using UniRx;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerAttackController : MonoBehaviour
    {
        public void Initialize(PlayerView player,
                                MainGameRepository mainGameRepository,
                                ICameraEntity cameraEntity,
                                IEnemiesView enemiesView)
        {
            IPlayerAttackUseCase playerAttackUseCase = new PlayerAttackActor(player, 
                                                                            mainGameRepository.Player,
                                                                            cameraEntity,
                                                                            enemiesView);

            Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("Attack"))
                .Subscribe(_ => {
                    playerAttackUseCase.AttackPlayer();
                }).AddTo(this);
        }
    }
}
