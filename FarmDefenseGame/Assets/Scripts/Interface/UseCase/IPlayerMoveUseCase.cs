using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IPlayerMoveUseCase : IMoveable
    {
        IPlayerView PlayerView { get; }
        IPlayerEntity PlayerEntity { get; }
        ICameraView Camera { get; }
    }

    public interface IMoveable
    {
        void MovePlayer(float horizontalInput, float verticalInput);
        void StandPlayer();
    }
}