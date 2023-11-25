using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace WolfVillageBattle.Interface
{
    public interface IEnemyView : IAnimation, IEnemyAnimation, IEnemySound, ISound
    {
        GameObject Body { get; }
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }
        IObservable<Unit> StartAttackObservable { get; }
        IObservable<Unit> StopAttackObservable { get; }
    }

    public interface IEnemyAnimation 
    {
        void PlayNotice();
        // 見逃す
        void PlayOverlook();
    }

    public interface IEnemySound
    {
        void PlayNoticeSound();
        void PlayOverlookSound();
    }
}

