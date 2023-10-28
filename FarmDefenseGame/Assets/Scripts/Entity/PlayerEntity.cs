using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerEntity : IPlayerEntity
    {
        public PlayerEntity(IPlayerVO playerVO)
        {
            this.playerVO = (PlayerVO) playerVO; 
            current_hp = playerVO.MaxHP;
            current_status = Status.IDLE;
        }

        [SerializeField]
        private PlayerVO playerVO;
        public IPlayerVO PlayerVO => playerVO;

        [SerializeField]
        private int current_hp;
        public int CurrentHP => current_hp;

        [SerializeField]
        private int attack;
        public int Attack => attack;

        // TODO: 後で考える(絶対にstringではなくなる) 
        [SerializeField]
        // string weapon;
        // public string Weapon => weapon;
        public string Weapon => throw new System.NotImplementedException();

        private Status current_status;
        public Status CurrentStatus => current_status;

        public void SetStatus(Status status)
        {
            current_status = status;
        }
    }
}