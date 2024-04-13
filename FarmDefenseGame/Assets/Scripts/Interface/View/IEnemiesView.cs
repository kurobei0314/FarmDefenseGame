using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public interface IEnemiesView 
    {
        GameObject GetMinDistanceEnemyFromPlayer();
        GameObject GetNeighborsEnemy(float cameraInput, Transform targetEnemy, Vector3 cameraPositionVector ,Vector3 rightCameraVector);
    }
}

