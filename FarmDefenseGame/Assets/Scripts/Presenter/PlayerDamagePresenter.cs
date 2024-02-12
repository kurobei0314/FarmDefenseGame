using UnityEngine;
using UniRx;
using UniRx.Triggers;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerDamagePresenter 
    {
        private IPlayerView playerView;
        private IPlayerEntity playerEntity;

        public PlayerDamagePresenter(IPlayerView playerView, IPlayerEntity playerEntity)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;

            IPlayerDamageUseCase playerDamageUseCase = new PlayerDamageActor(playerView, playerEntity);

            playerView.GameObject.OnCollisionEnterAsObservable()
                                .Where(collision => collision.gameObject.tag == "EnemyBody")
                                .Subscribe(enemy => {
                                    if (playerEntity.CurrentStatus == Status.DAMAGE || playerEntity.CurrentStatus == Status.DIE) return;
                                    var parent = enemy.gameObject.transform.parent;
                                    if (parent == null || parent.gameObject.tag != "Enemy") return;
                                    var enemyView = parent.gameObject.GetComponent<EnemyView>();
                                    if (!enemyView) 
                                    {
                                        Debug.LogError("enemyViewが取得できませんでした");
                                        return;
                                    }
                                    if (enemyView.EnemyEntity.CurrentStatus != Status.ATTACK) return;
                                    playerDamageUseCase.ReduceHP(enemyView.EnemyEntity.EnemyVO.Attack);
                                });

            playerEntity.CurrentHP
                .Pairwise()
                .Where(value => value.Current < value.Previous)
                .Subscribe(hp => {
                    if (hp.Current <= 0)
                    {
                        playerDamageUseCase.Die();
                    }
                    else
                    {
                        playerDamageUseCase.Damage();
                    }
            }).AddTo(playerView.GameObject);
        }
    
    }
}
