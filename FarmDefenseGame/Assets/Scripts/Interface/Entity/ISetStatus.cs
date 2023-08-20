using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface ISetStatus
    {
        Status CurrentStatus { get; }
        void SetStatus(Status status);
    }
}