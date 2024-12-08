using WolfVillage.Entity.Interface;

namespace WolfVillageBattle.Interface
{
    public interface IEnemyDamageUseCase 
    {
        void ReduceHP(IWeaponEntity setWeapon);
        void Damage();
        void Die();
    }
}

