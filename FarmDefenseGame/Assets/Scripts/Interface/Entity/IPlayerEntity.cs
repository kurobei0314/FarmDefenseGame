using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public enum Status
    {
        IDLE,
        ATTACK,
        DAMAGE,
        DIE,
        WIN, // これはいらない？
    }

    public interface IPlayerEntity : ISetStatus
    {
        IPlayerVO PlayerVO { get; }
        int CurrentHP { get; }
        int Attack { get; }
        string Weapon { get; }
    }
}
