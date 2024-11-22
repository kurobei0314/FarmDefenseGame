using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
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