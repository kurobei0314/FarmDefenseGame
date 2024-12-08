using System;
using UnityEngine;
using WolfVillage.ValueObject.Interface;
using WolfVillage.Interface;

namespace WolfVillage.ValueObject
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

        [SerializeField]
        private string description;
        public string Description => description;
    }
}
