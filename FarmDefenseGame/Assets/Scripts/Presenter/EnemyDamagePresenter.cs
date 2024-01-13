using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyDamagePresenter
    {
        private IEnemyView enemyView;
        private IEnemyEntity enemyEntity;
        private IPlayerEntity playerEntity;

        public EnemyDamagePresenter(IEnemyView enemyView, IEnemyEntity enemyEntity, IPlayerEntity playerEntity)
        {
            this.enemyView = enemyView;
            this.enemyEntity = enemyEntity;
            this.playerEntity = playerEntity;
            
            IEnemyDamageUseCase damageActor = new EnemyDamageActor(enemyView, enemyEntity);
            enemyView.Body.OnCollisionEnterAsObservable()
                            .Where(_ => _.gameObject.tag == "PlayerWeapon")
                            .Subscribe(_ => {
                                if (playerEntity.CurrentStatus != Status.ATTACK) return;
                                if (enemyEntity.CurrentStatus == Status.DAMAGE 
                                    || enemyEntity.CurrentStatus == Status.DIE) return;
                                damageActor.ReduceHP(playerEntity.Attack);
                            }).AddTo(enemyView.GameObject);

            enemyEntity.CurrentHP
                .Pairwise()
                .Where(value => value.Current < value.Previous)
                .Subscribe(hp => {
                    if (hp.Current <= 0)
                    {
                        damageActor.Die();
                    }
                    else
                    {
                        damageActor.Damage();
                    }
            }).AddTo(enemyView.GameObject);
        }
        // // プレイヤーが攻撃した時の挙動(Enterにしたいけど、今のcolliderの設定的にstayの方がいいかもしれない)
        // Body.OnTriggerStayAsObservable()
        //     .Where(_ =>  _.gameObject.tag == "PlayerWeapon")
        //     .Subscribe(_ => {
        //         var root_gameObject = _.gameObject.transform.root.gameObject;
        //         if (root_gameObject.GetComponent<PlayerTest>().CurrentStatus != PlayerTest.PlayerStatus.ATTACK) return;
        //         if (current_enemy_status == EnemyStatus.DAMAGE || current_enemy_status == EnemyStatus.DIE) return;
        //         Debug.Log("今のcurrent_enemy_status:"+ current_enemy_status);
        //         Debug.Log("攻撃したおー");
        //         Debug.Log("current_enemy_status:"+ current_enemy_status);
        //         ReserveDamage(root_gameObject.GetComponent<PlayerTest>().Attack);
        //         UpdateHPbar(hp/(float)max_hp);
        //         if (hp == 0){
        //             Debug.Log("倒れたー");
        //             SetEnemyStatus(EnemyStatus.DIE);
        //             AudioManager.Instance.PlaySE("slime_die");
        //             GetComponent<Animator>().Play("Die");
        //             enemyDie.OnNext(1);
        //         }
        //         else {
        //             Debug.Log("ダメージ受けたー");
        //             GetComponent<Animator>().Play("GetHit");
        //             SetEnemyStatus(EnemyStatus.DAMAGE);
        //         }
        // }).AddTo(this);
    }
}

