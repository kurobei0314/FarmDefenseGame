using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IWeaponVO
    {
        string Name { get; }
        int Id { get; }
        RoleType RoleType { get; }
        AttackType AttackType { get; }
    }
}
