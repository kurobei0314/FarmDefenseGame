using R3;
using WolfVillage.Battle;
using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity
{
    public class BattlePlayerEntity : PlayerEntity, IBattlePlayerEntity
    {
        public BattlePlayerEntity ( IPlayerStatusVO playerStatusVO,
                                    ISkillEntity[] skillEntities,
                                    IWeaponEntity weaponEntity,
                                    IArmorEntity armorEntity) : base( playerStatusVO, skillEntities, weaponEntity, armorEntity)
        {
            setCurrentSkills = new ISkillEntity[BattleGameInfo.PLAYER_SET_SKILL_NUM];

            current_hp = new ReactiveProperty<int>();
            current_hp.Value = current_max_hp;
            current_status = Status.Idle;
        }

        private ReactiveProperty<int> current_hp;
        public ReactiveProperty<int> CurrentHP => current_hp;
        public int CurrentHPValue => current_hp.Value;

        private Status current_status;
        public Status CurrentStatus => current_status;

        public void ReduceHP(int value)
        {
            current_hp.Value = (current_hp.Value - value <= 0) ? 0 : current_hp.Value - value;
        }

        public bool IsAttack()
            => current_status == Status.Attack || current_status == Status.JustAvoidAttack;

        public void SetStatus(Status status)
            => current_status = status;

    }
}
