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

        void PlayNotice();
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

