using System;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    [Serializable]
    public class FieldEnemyVO : IFieldEnemyVO
    {
        [SerializeField] private int id;
        public int Id => id;

        [SerializeField] private int field_id;
        public int FieldId => field_id;

        [SerializeField] private int enemy_id;
        public int EnemyId => enemy_id;

        [SerializeField] private int pos_x;
        public int PosX => pos_x;

        [SerializeField] private int pos_z;
        public int PosZ => pos_z;

        [SerializeField] private int rotation_y;
        public int RotationY => rotation_y;
    }
}
