using System;
using UnityEngine;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.ValueObject
{
    [Serializable]
    public class FieldVO : IFieldVO
    {
        [SerializeField] private string name;
        public string Name => name;

        [SerializeField] private int id;
        public int Id => id;

        [SerializeField] private string scene_name;
        public string SceneName => scene_name;

        [SerializeField] private float player_pos_x;
        [SerializeField] private float player_pos_y;
        [SerializeField] private float player_pos_z;
        public Vector3 PlayerInitPos => new Vector3(player_pos_x, player_pos_y, player_pos_z);

        [SerializeField] private float player_rot_y;
        public Vector3 PlayerInitRot => new Vector3(0.0f, player_rot_y, 0.0f);
    }
}

