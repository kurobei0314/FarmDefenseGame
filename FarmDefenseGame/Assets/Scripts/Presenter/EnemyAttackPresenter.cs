using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;
using UniRx;
namespace WolfVillageBattle
{
    public class EnemyAttackPresenter
    {
        public EnemyAttackPresenter(IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            IEnemyAttackUseCase attackUseCase = new EnemyAttackActor(enemyView, enemyEntity);

            enemyView.StartAttackObservable.Subscribe(_ => {
                attackUseCase.StartAttack();
            }).AddTo(enemyView.GameObject);

            enemyView.StopAttackObservable.Subscribe(_ => {
                attackUseCase.StopAttack();
            }).AddTo(enemyView.GameObject);
        }
    }
}

