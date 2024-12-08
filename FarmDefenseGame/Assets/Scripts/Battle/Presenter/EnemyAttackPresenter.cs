using WolfVillage.Battle.Interface;
using R3;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle
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

