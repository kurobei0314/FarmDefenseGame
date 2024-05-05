using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace WolfVillageBattle.Interface
{
    public interface IEnemyView 
    {
        GameObject Body { get; }
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }
        IEnemyEntity EnemyEntity { get; }
        IObservable<Unit> StartAttackObservable { get; }
        IObservable<Unit> StopAttackObservable { get; }
        Boolean IsVisible(ICameraView cameraView);
        void Notice();
        void Outlook();
        void Attack();
        void Damage();
        void Die();
        void SetTargetLockActive(bool active);
    }

    public interface IEnemyAnimation 
    {
        void PlayNoticeAnim();
        // 見逃す
        void PlayOverlookAnim();
    }

    public interface IEnemySound
    {
        void PlayNoticeSound();
        void PlayOverlookSound();
    }
}

