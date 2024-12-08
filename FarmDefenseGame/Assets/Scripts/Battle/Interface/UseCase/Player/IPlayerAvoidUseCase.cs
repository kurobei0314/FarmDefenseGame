using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillage.Battle.Interface
{
    public interface IPlayerAvoidUseCase
    {
        void AvoidEnemy(float horizontalInput, float verticalInput);
    }
}
