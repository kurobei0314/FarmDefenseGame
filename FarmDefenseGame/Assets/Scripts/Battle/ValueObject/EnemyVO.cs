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
        private string name;
        public string Name => name;
        
        [SerializeField]
        private int id;
        public int Id => id;

        [SerializeField]
        private int max_hp;
        public int MaxHP => max_hp;

        [SerializeField]
        private int attack;
        public int Attack => attack;

        [SerializeField]
        private string prefab_name;
        public string PrefabName => prefab_name;

        [SerializeField]
        private int attack_type_id;
        public AttackType AttackType => (AttackType)attack_type_id;

        [SerializeField]
        private string description;
        public string Description => description;
    }
}
