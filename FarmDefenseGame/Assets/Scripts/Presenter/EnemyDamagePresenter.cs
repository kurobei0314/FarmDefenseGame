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
                                if (!playerEntity.IsAttack()) return;
                                if (enemyEntity.CurrentStatus == Status.Damage 
                                    || enemyEntity.CurrentStatus == Status.Die) return;
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
    }
}

