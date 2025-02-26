using WolfVillage.Entity.Interface;
using WolfVillage.Battle.Interface;

namespace WolfVillage.Battle
{
    public class EnemyAttackActor : IEnemyAttackUseCase
    {
        private IEnemyView enemyView;
        private IEnemyEntity enemyEntity;

        public EnemyAttackActor (IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            this.enemyView = enemyView;
            this.enemyEntity = enemyEntity;
        }

        public void StartAttack()
        {
            if (enemyEntity.CurrentStatus != Status.Notice) return;
            enemyEntity.SetStatus(Status.Attack);
            enemyView.Attack();
        }
        
        public void StopAttack()
        {
            enemyEntity.SetStatus(Status.Notice);
        }
    }
}
