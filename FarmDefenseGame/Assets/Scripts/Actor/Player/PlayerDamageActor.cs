using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerDamageActor : IPlayerDamageUseCase
    {
        private IPlayerView playerView;
        private IPlayerEntity playerEntity;
        private IEnemyEntity enemyEntity;

        public PlayerDamageActor(IPlayerView playerView, IPlayerEntity playerEntity, IEnemyEntity enemyEntity)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
            this.enemyEntity = enemyEntity;
        }

        public void ReduceHP (int damage)
        {

        }

        public void Damage()
        {

        }

        public void Die()
        {

        }
    }
    
}
