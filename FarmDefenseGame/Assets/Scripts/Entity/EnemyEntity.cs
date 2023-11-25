using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyEntity : IEnemyEntity
    {
        public EnemyEntity(IEnemyVO enemyVO)
        {
            this.enemyVO = (EnemyVO) enemyVO; 
            current_hp = enemyVO.MaxHP;
            current_status = Status.IDLE;
        }

        [SerializeField]
        private EnemyVO enemyVO;
        public IEnemyVO EnemyVO => enemyVO;

        [SerializeField]
        private int current_hp;
        public int CurrentHP => current_hp;

        private Status current_status;
        public Status CurrentStatus => current_status;

        public void SetStatus(Status status)
        {
            current_status = status;
        }
    }
}
