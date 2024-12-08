using UnityEngine;
using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject;

namespace WolfVillage.Entity
{
    public class ItemEntity : IItemEntity
    {
        [SerializeField]
        private ItemVO itemVO;
        public ItemVO ItemVO => itemVO;

        [SerializeField]
        private int num;
        public int Num => num;
    }
}