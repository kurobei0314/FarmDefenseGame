using WolfVillage.Entity.Interface;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyNoticeActor : IEnemyNoticeUseCase
    {
        private IEnemyEntity enemyEntity;
        private IEnemyView enemyView;

        public EnemyNoticeActor (IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            this.enemyView = enemyView;
            this.enemyEntity = enemyEntity; 
        }

        public void EnemyNotice()
        {
            enemyEntity.SetStatus(Status.Notice);
            enemyView.Notice();
        }

        public void EnemyOverlook()
        {
            enemyEntity.SetStatus(Status.Idle);
            enemyView.Outlook();
        }
    }
}

