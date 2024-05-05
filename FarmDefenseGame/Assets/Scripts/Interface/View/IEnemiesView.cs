using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IEnemiesView 
    {
        IEnemyView GetMinDistanceEnemyFromPlayer(ICameraView cameraView);
        IEnemyView GetNeighborsEnemy(float cameraInput, Transform targetEnemy, ICameraView cameraView, Vector3 playerPositionVector, Vector3 rightPlayerVector);
    }
}

