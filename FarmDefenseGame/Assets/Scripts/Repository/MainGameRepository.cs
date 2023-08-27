using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;
using System;

namespace WolfVillageBattle.Interface
{
    public interface IMainGameRepository
    {
        IPlayerEntity Player{ get; }
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
        
        private IPlayerEntity player;
        public IPlayerEntity Player => player;

        public void Initialize()
        {
            player = new PlayerEntity(playerDataStore.Items[0]);
        }
    }
}
