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
// // 敵からの攻撃を受けた時の挙動
//         body.OnTriggerEnterAsObservable()
//             .Subscribe(_ => {
//                 var parent = _.gameObject.transform.parent;
//                 if (parent == null || parent.gameObject.tag != "Enemy") return;
//                 // TODO：これも絶対仕組み変える絶対に
//                 if (CurrentStatus != PlayerStatus.IDLE) return;
//                 var enemy_status = parent.gameObject.GetComponent<EnemyTest>().current_enemy_status;
//                 if (enemy_status != EnemyTest.EnemyStatus.ATTACK) return;
//                 Debug.Log("敵から攻撃されたおー");
//                 ReserveDamage(parent.gameObject.GetComponent<EnemyTest>().Attack);
//                 UpdateHPbar(hp/(float)max_hp);
//                 if (hp == 0) {
//                     SetPlayerStatus(PlayerStatus.DIE);
//                     Debug.Log("HPが0になっちゃったー");
//                     AudioManager.Instance.PlaySE("unitychan_lose");
//                     unity_chan.GetComponent<Animator>().Play("GoDown");
//                     playerDie.OnNext(1);
//                 }
//                 else {
//                     float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
//                     if (rnd < 0.5f){
//                         AudioManager.Instance.PlaySE("unitychan_damage1");
//                     } else{
//                         AudioManager.Instance.PlaySE("unitychan_damage2");
//                     }
//                     StartCoroutine ("PlayAnimDamage");
//                 }
//         }).AddTo(this);