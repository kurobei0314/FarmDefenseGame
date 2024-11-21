using UnityEngine;
using WolfVillageBattle.Interface;
using System;

namespace WolfVillageBattle
{
    [Serializable]
    public class EquipmentVO : IEquipmentVO
    {
        [SerializeField]
        private string name;
        public string Name => name;
        
        [SerializeField]
        private int id;
        public int Id => id;

        [SerializeField]
        private int defense;
        public int Defense => defense;
    }
}


