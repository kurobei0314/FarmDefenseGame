using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerAttackActor : IPlayerAttackUseCase
    {
        private IPlayerView playerView;
        private IPlayerEntity playerEntity;

        public PlayerAttackActor(IPlayerView playerView, IPlayerEntity playerEntity)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
        }
        public void AttackPlayer()
        {
            if (playerEntity.CurrentStatus != Status.Idle && playerEntity.CurrentStatus != Status.Attack) return;
            playerEntity.SetStatus(Status.Attack);
            playerView.Attack();
        }
    }
}

