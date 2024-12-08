using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillage.Battle.Interface
{
    public interface IEnemiesView 
    {
        IEnemyView TargetEnemyView { get; }
        IEnemyView HitEnemyView { get; }

        void SetHitEnemyView(IEnemyView enemyView);
        void SetTargetEnemyView(IEnemyView enemyView);

        IEnemyView GetMinDistanceEnemyFromPlayer(ICameraView cameraView);
        IEnemyView GetNeighborsEnemy(float cameraInput, IEnemyView targetEnemy, ICameraView cameraView);
    }
}

