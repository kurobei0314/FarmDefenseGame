using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerEntity : IPlayerEntity
    {
        public PlayerEntity(IPlayerStatusVO playerStatusVO,
                            ISkillEntity[] skillEntities,
                            IWeaponEntity weaponEntity,
                            IArmorEntity armorEntity)
        {
            setCurrentSkills = new ISkillEntity[GameInfo.PLAYER_SET_SKILL_NUM];
            for (var i = 0 ; i < setCurrentSkills.Length; i++)
            {
                setCurrentSkills[i] = (skillEntities.Length > i) ? skillEntities[i] : null;
            }
            setCurrentWeapon = weaponEntity;
            setCurrentArmor = armorEntity;
            this.playerStatusVO = (PlayerStatusVO) playerStatusVO; 
            // TODO: 武器の特性も見てMaxHPを加算する処理を作る
            current_max_hp = playerStatusVO.MaxHP;
            current_hp = new ReactiveProperty<int>();
            current_hp.Value = current_max_hp;
            current_status = Status.Idle;
        }

        private PlayerStatusVO playerStatusVO;
        public IPlayerStatusVO PlayerStatusVO => playerStatusVO;

        private ReactiveProperty<int> current_hp;
        public ReactiveProperty<int> CurrentHP => current_hp;
        public int CurrentHPValue => current_hp.Value;

        // MEMO: 武器によって最大HPが変化する可能性もあるため、一旦Entityに持ってる
        private int current_max_hp;
        public int CurrentMaxHP => current_max_hp;

        // TODO: 武器をセットできるようにする(今は適当)
        private IWeaponEntity setCurrentWeapon;
        public IWeaponEntity SetCurrentWeapon => setCurrentWeapon;

        // TODO: 防具をセットできるようにする(今は適当)
        private IArmorEntity setCurrentArmor;
        public IArmorEntity SetCurrentArmor => setCurrentArmor;

        // TODO: スキルをセットできるようにする(今は適当)
        private ISkillEntity[] setCurrentSkills;
        public ISkillEntity[] SetCurrentSkills => setCurrentSkills;

        private Status current_status;
        public Status CurrentStatus => current_status;

        public void ReduceHP(int value)
        {
            current_hp.Value = (current_hp.Value - value <= 0) ? 0 : current_hp.Value - value;
        }

        public void SetStatus(Status status)
            => current_status = status;

        public bool IsAttack()
            => current_status == Status.Attack || current_status == Status.JustAvoidAttack;
    }
}