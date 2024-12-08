using UnityEngine;
using WolfVillage.ValueObject.Interface;
using System;

namespace WolfVillage.ValueObject
{
    [Serializable]
    public class PlayerStatusVO : IPlayerStatusVO
    {
        [SerializeField]
        private int id;
        public int Id => id;

        [SerializeField]
        private int clear_field_id;
        public int ClearFieldId => clear_field_id;

        [SerializeField]
        private int max_hp;
        public int MaxHP => max_hp;
    }
}
