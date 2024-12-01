using UnityEngine;
using R3;
using R3.Triggers;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerDamagePresenter 
    {
        private IPlayerView playerView;
        private IPlayerEntity playerEntity;

        public PlayerDamagePresenter(IPlayerView playerView, IPlayerEntity playerEntity, IInGameView inGameView, IEnemiesView enemiesView)
        {
            IPlayerDamageUseCase playerDamageUseCase = new PlayerDamageActor(playerView, playerEntity, inGameView, enemiesView);

            playerView.GameObject.OnCollisionEnterAsObservable()
                                .Where(collision => collision.gameObject.tag == "EnemyBody")
                                .Subscribe(enemy => {
                                    playerDamageUseCase.HitEnemyAttack(enemy);
                                });

            playerEntity.CurrentHP
                .Pairwise()
                .Where(value => value.Current < value.Previous)
                .Subscribe(hp => {
                    if (hp.Current <= 0) playerDamageUseCase.Die();
                    else                 playerDamageUseCase.Damage();
            }).AddTo(playerView.GameObject);
        }
    
    }
}
