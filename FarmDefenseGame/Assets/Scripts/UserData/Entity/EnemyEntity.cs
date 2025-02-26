using UnityEngine;
using R3;
using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject.Interface;
using WolfVillage.ValueObject;

namespace WolfVillage.Entity
{
    public class EnemyEntity : IEnemyEntity
    {
        public EnemyEntity(IEnemyVO enemyVO)
        {
            this.enemyVO = (EnemyVO) enemyVO; 
            current_hp = new ReactiveProperty<int>();
            current_hp.Value = enemyVO.MaxHP;
            current_status = Status.Idle;
        }

        [SerializeField]
        private EnemyVO enemyVO;
        public IEnemyVO EnemyVO => enemyVO;

        [SerializeField]
        private ReactiveProperty<int> current_hp;
        public ReactiveProperty<int> CurrentHP => current_hp;

        public int CurrentHPValue => current_hp.Value;

        private Status current_status;
        public Status CurrentStatus => current_status;

        public void ReduceHP(int value)
        {
            current_hp.Value = (current_hp.Value - value <= 0) ? 0 : current_hp.Value - value;
        }

        public void SetStatus(Status status)
        {
            current_status = status;
        }
    }
}
