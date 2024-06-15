using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerEntity : IPlayerEntity
    {
        public PlayerEntity(IPlayerVO playerVO)
        {
            this.playerVO = (PlayerVO) playerVO; 
            current_hp = new ReactiveProperty<int>();
            current_hp.Value = playerVO.MaxHP;
            current_status = Status.Idle;
        }

        [SerializeField]
        private PlayerVO playerVO;
        public IPlayerVO PlayerVO => playerVO;

        [SerializeField]
        private ReactiveProperty<int> current_hp;
        public ReactiveProperty<int> CurrentHP => current_hp;
        public int CurrentHPValue => current_hp.Value;

        // TODO: weaponに応じた攻撃力になるようにする(今は固定で1)
        [SerializeField]
        private int attack = 1;
        public int Attack => attack;

        // TODO: 後で考える(絶対にstringではなくなる) 
        [SerializeField]
        // string weapon;
        // public string Weapon => weapon;
        public string Weapon => throw new System.NotImplementedException();

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