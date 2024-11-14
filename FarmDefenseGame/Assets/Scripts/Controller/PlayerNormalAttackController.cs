using UnityEngine;
using R3;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class PlayerNormalAttackController : MonoBehaviour
    {
        public void Initialize(PlayerView player,
                                IPlayerEntity playerEntity,
                                ICameraEntity cameraEntity,
                                IEnemiesView enemiesView)
        {
            IPlayerNormalAttackUseCase playerAttackUseCase = new PlayerNormalAttackActor(player, 
                                                                            playerEntity,
                                                                            cameraEntity,
                                                                            enemiesView);

            Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("NormalAttack"))
                .Subscribe(_ => {
                    playerAttackUseCase.AttackPlayer();
                }).AddTo(this);
        }
    }
}
