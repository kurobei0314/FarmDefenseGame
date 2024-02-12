using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;
using System;

namespace WolfVillageBattle
{
    [Serializable]
    public class EnemyVO : IEnemyVO
    {
        [SerializeField]
        string name;
        public string Name => name;
        
        [SerializeField]
        int id;
        public int Id => id;

        [SerializeField]
        int max_hp;
        public int MaxHP => max_hp;

        [SerializeField]
        int attack;
        public int Attack => attack;

        [SerializeField]
        string prefab_name;
        public string PrefabName => prefab_name;

    }
}
