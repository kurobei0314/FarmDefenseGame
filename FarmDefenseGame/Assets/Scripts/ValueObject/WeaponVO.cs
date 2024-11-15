using System;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    [Serializable]
    public class WeaponVO : IWeaponVO
    {
        [SerializeField]
        private string name;
        public string Name => name;

        [SerializeField]
        private int id;
        public int Id => id;

        [SerializeField]
        private int attack;
        public int Attack => attack;

        [SerializeField]
        private int role_type_id;
        public RoleType RoleType => (RoleType)role_type_id;

        [SerializeField]
        private int attack_type_id;
        public AttackType AttackType => (AttackType)attack_type_id;
    }
}
