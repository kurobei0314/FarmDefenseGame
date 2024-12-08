using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle.Interface
{
    public interface IEnemyDamageUseCase 
    {
        void ReduceHP(IWeaponEntity setWeapon);
        void Damage();
        void Die();
    }
}

