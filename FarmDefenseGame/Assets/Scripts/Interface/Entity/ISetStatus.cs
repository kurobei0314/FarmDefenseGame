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

        WIN, // player専用

        NOTICE, // Enemy専用
    }
    
    public interface ISetStatus
    {
        Status CurrentStatus { get; }
        void SetStatus(Status status);
    }
}