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
            if (   playerEntity.CurrentStatus != Status.Idle 
                && playerEntity.CurrentStatus != Status.Attack
                && playerEntity.CurrentStatus != Status.JustAvoid) return;
            
            if (playerEntity.CurrentStatus == Status.JustAvoid) JustAvoidAttack();
            else                                                NormalAttack();
        }

        private void NormalAttack()
        {
            playerEntity.SetStatus(Status.Attack);
            playerView.Attack();
        }

        private void JustAvoidAttack()
        {
            // TODO: 一旦Attackで代用
            playerEntity.SetStatus(Status.Attack);
            playerView.Attack();
            var timeScaler = (ITimeScaler) new TimeScaler();
            timeScaler.SetTimeScaler(1.0f);
        }
    }
}

