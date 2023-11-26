using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyMoveAI enemyMoveAI;
        private PlayerView playerView;
        private EnemyView enemyView;
        private EnemyEntity enemyEntity;
        // public void Initialize(PlayerView playerView, EnemyView enemyView, EnemyEntity enemyEntity)
        // {
        //     this.playerView = (PlayerView)playerView;
        //     this.enemyView = (EnemyView) enemyView;
        //     this.enemyEntity = (EnemyEntity) enemyEntity;

        //     Observable.Timer(TimeSpan.FromSeconds(1.0f))
        //         .Subscribe(_=> {
        //             UpdateAI();
        //         }).AddTo(this);
        // }

        // private void UpdateAI()
        // {
        //     if (enemyMoveAI.IsStopped) return;
        //     switch (enemyEntity.CurrentStatus)
        //     {
        //         case Status.IDLE:
        //             enemyMoveAI.SetIdleAI();
        //             break;
        //         case Status.NOTICE:
        //             enemyMoveAI.SetNoticeAI();
        //             break;
        //     }
        // }
    }
}
