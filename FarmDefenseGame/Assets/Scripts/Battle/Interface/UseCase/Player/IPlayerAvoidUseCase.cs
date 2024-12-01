using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerAvoidUseCase
    {
        void AvoidEnemy(float horizontalInput, float verticalInput);
    }
}
