using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IEnemyVO 
    {
        string Name { get; }
        int ID { get; }
        int MaxHP { get; }
        int Attack { get; }
        string PrefabName { get; }
    }

}
