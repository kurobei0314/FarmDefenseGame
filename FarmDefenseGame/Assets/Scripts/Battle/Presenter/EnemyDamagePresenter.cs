using R3;
using R3.Triggers;
using WolfVillage.Entity.Interface;
using WolfVillage.Battle.Interface;

namespace WolfVillage.Battle
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
                                damageActor.ReduceHP(playerEntity.SetCurrentWeapon);
                            }).AddTo(enemyView.GameObject);

            enemyEntity.CurrentHP
                .Pairwise()
                .Where(value => value.Current < value.Previous)
                .Subscribe(hp => {
                    if (hp.Current <= 0) damageActor.Die();
                    else                 damageActor.Damage();
            }).AddTo(enemyView.GameObject);
        }
    }
}

