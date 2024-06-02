using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

public class PlayerAvoidActor : IPlayerAvoidUseCase
{
    private IPlayerView _playerView;
    private IPlayerEntity _playerEntity;

    public PlayerAvoidActor(IPlayerView playerView, IPlayerEntity playerEntity)
    {
        _playerView = playerView;
        _playerEntity = playerEntity;
    }
    
    public void AvoidEnemy()
    {
        if (_playerEntity.CurrentStatus != Status.Idle) return;
        _playerEntity.SetStatus(Status.Avoid);
        _playerView.Avoid();
    }
}
