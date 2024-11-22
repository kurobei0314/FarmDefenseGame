using System;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    [Serializable]
    public class ItemVO : IItemVO
    {
        [SerializeField]
        private string name;
        public string Name => name;

        [SerializeField]
        private int id;
        public int Id => id;

        [SerializeField]
        private string description;
        public string Description => description;
    }
}
