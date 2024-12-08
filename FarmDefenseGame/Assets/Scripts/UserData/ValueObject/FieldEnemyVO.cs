using System;
using UnityEngine;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.ValueObject
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

        [SerializeField] private float pos_x;
        public float PosX => pos_x;

        [SerializeField] private float pos_z;
        public float PosZ => pos_z;

        [SerializeField] private float rotation_y;
        public float RotationY => rotation_y;
    }
}
