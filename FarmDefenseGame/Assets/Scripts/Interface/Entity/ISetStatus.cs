using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public enum Status
    {
        Idle,
        Attack,
        Damage,
        Die,
        Avoid,
        Win, // player専用

        Notice, // Enemy専用
    }
    
    public interface ISetStatus
    {
        Status CurrentStatus { get; }
        void SetStatus(Status status);
    }
}