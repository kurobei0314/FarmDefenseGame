using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface 
{
    public interface IPlayerView
    {
        GameObject unityChan { get; }
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }
        GameObject Body { get; }
        void Walk();
        void Stand();
        void Attack();
    }

    public interface IAnimation 
    {
        Animator Animator { get; }
        void Walk();
        void Stand();
        void Attack();
    }

    public interface ISound
    {
        void PlayAttackSound();
    }
}
