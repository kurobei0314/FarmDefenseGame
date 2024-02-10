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
        void MovePlayer(float horizontalInput, float verticalInput);
        void StandPlayer();
    }
}