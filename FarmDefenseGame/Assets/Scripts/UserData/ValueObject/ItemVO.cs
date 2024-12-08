using System;
using UnityEngine;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.ValueObject
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
