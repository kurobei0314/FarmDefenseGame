using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface 
{
    public interface IPlayerView : IAnimation, ISound
    {
        GameObject unityChan { get; }
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }
    }

    public interface IAnimation 
    {
        Animator Animator { get; }
        void PlayWalkAnim();
        void PlayStandAnim();
        void PlayAttackAnim();
    }

    public interface ISound
    {
        void PlayAttackSound();
    }
}
