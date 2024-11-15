using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerEntity : IPlayerEntity
    {
        public PlayerEntity(IPlayerVO playerVO, ISkillEntity[] skillEntities)
        {
            this.playerVO = (PlayerVO) playerVO; 
            current_hp = new ReactiveProperty<int>();
            current_hp.Value = playerVO.MaxHP;
            current_status = Status.Idle;
            setCurrentSkills = new ISkillEntity[GameInfo.PLAYER_SET_SKILL_NUM];

            for (var i = 0 ; i < setCurrentSkills.Length; i++)
            {
                setCurrentSkills[i] = (skillEntities.Length > i) ? skillEntities[i] : null;
            }
        }

        private PlayerVO playerVO;
        public IPlayerVO PlayerVO => playerVO;

        private ReactiveProperty<int> current_hp;
        public ReactiveProperty<int> CurrentHP => current_hp;
        public int CurrentHPValue => current_hp.Value;

        // TODO: weaponに応じた攻撃力になるようにする(今は固定で1)
        private int attack = 1;
        public int Attack => attack;

        // TODO: 武器をセットできるようにする(今は適当)
        public IWeaponEntity SetCurrentWeapon => throw new System.NotImplementedException();

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