using System;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
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
    }
}

