using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IEnemyEntity : ISetStatus
    {
        IEnemyVO EnemyVO { get; }
        int CurrentHP { get; }
    }
}

