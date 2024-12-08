using UnityEngine;
using WolfVillage.Entity.Interface;
using WolfVillage.Battle.Interface;
using WolfVillage.Interface;

namespace WolfVillage.Battle
{
    public class EnemyDamageActor : IEnemyDamageUseCase
    {
        private IEnemyView enemyView;
        private IEnemyEntity enemyEntity;

        public EnemyDamageActor (IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            this.enemyView = enemyView;
            this.enemyEntity = enemyEntity;
        }

        public void ReduceHP(IWeaponEntity setWeapon)
        {
            var damage = CalculateTotalEnemyDamage(setWeapon);
            enemyEntity.ReduceHP(damage);
        }

        private int CalculateTotalEnemyDamage(IWeaponEntity setWeapon)
            => CalculateEnemyDamage(setWeapon) + (Random.Range(0, 21) - 10);

        private int CalculateEnemyDamage(IWeaponEntity setWeapon)
        {
            if (setWeapon.WeaponVO.AttackType == AttackType.White)
            {
                return (int) GameInfo.ATTACK_GLOBAL_TYPE_RATIO * setWeapon.WeaponVO.Attack ;
            } 
            if (setWeapon.WeaponVO.AttackType == enemyEntity.EnemyVO.AttackType)
            {
                return (int) GameInfo.ATTACK_WEAK_TYPE_RATIO * setWeapon.WeaponVO.Attack;
            }
            return (int) setWeapon.WeaponVO.Attack;
        }

        public void Damage()
        {
            enemyView.Damage();
            enemyEntity.SetStatus(Status.Damage);
        }
        
        public void Die()
        {
            enemyView.Die();
            enemyEntity.SetStatus(Status.Die);
        }
    }
}
