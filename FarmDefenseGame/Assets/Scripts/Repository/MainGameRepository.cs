using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;
using System;

namespace WolfVillageBattle.Interface
{
    public interface IMainGameRepository
    {
        IPlayerEntity Player { get; }
        IEnemyEntity[] Enemies { get; }
        void Initialize();
    }
}

namespace WolfVillageBattle
{
    [CreateAssetMenu]
    public class MainGameRepository : ScriptableObject, IMainGameRepository
    {
        [SerializeField]
        private PlayerVODataStore playerDataStore;

        [SerializeField]
        private EnemyVODataStore enemyVODataStore;
        
        private IPlayerEntity player;
        public IPlayerEntity Player => player;

        private IEnemyEntity[] enemies;
        public IEnemyEntity[] Enemies => enemies;

        public void Initialize()
        {
            player = new PlayerEntity(playerDataStore.Items[0]);
            enemies = new IEnemyEntity[enemyVODataStore.Items.Count];
            
            for (int i = 0; i < enemyVODataStore.Items.Count; i++)
            {
                enemies[i] = new EnemyEntity(enemyVODataStore.Items[i]);
            }
        }
    }
}
