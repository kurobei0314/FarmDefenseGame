using UnityEngine;
using WolfVillage.ValueObject.Interface;
using System;

namespace WolfVillage.ValueObject
{
    [Serializable]
    public class ArmorVO : IArmorVO
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

        [SerializeField]
        private string description;
        public string Description => description;
    }
}


