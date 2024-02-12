using UnityEngine;
using WolfVillageBattle.Interface;
using System;

namespace WolfVillageBattle
{
    [Serializable]
    public class PlayerVO : IPlayerVO
    {
        [SerializeField]
        private int max_hp;
        public int MaxHP => max_hp;
    }
}
