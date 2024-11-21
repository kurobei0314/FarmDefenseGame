using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerStatusVO
    {
        int Id { get; }
        int ClearFieldId { get; }
        int MaxHP { get; }
    }
}
