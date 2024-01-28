using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace WolfVillageBattle.Interface
{
    public interface IEnemyEntity : ISetStatus
    {
        IEnemyVO EnemyVO { get; }
        ReactiveProperty<int> CurrentHP { get; }
        int CurrentHPValue { get; }
        public void ReduceHP(int value);
    }
}

