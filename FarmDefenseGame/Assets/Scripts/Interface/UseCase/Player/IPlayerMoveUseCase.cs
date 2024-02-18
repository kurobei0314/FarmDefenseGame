using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerMoveUseCase : IMoveable
    {
    }

    public interface IMoveable
    {
        void RunPlayer(float horizontalInput, float verticalInput);
        void WalkPlayer(float horizontalInput, float verticalInput);
        void StandPlayer();
    }
}