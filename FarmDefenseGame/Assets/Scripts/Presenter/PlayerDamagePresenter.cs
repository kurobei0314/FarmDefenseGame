using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerDamagePresenter 
    {
        private IPlayerView playerView;
        private IEnemyEntity enemyEntity;
        private IPlayerEntity playerEntity;

        public PlayerDamagePresenter(IPlayerView playerView, IPlayerEntity playerEntity, IEnemyEntity enemyEntity)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
            this.enemyEntity = enemyEntity;

            IPlayerDamageUseCase playerDamageUseCase = new PlayerDamageActor(playerView, playerEntity, enemyEntity);

            playerView.Body.OnCollisionEnterAsObservable().Subscribe(_ => {
                // var parent = _.gameObject.transform.parent;
                // if (parent == null || parent.gameObject.tag != "Enemy") return;
                // // TODO：これも絶対仕組み変える絶対に
                // if (CurrentStatus != PlayerStatus.IDLE) return;
                // var enemy_status = parent.gameObject.GetComponent<EnemyTest>().current_enemy_status;
                // if (enemy_status != EnemyTest.EnemyStatus.ATTACK) return;
                // Debug.Log("敵から攻撃されたおー");
                playerDamageUseCase.ReduceHP(enemyEntity.EnemyVO.Attack);
                // ReserveDamage(parent.gameObject.GetComponent<EnemyTest>().Attack);

            });
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