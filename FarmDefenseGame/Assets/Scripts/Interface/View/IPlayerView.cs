using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface 
{
    public interface IPlayerView : IAnimation
    {
        GameObject unityChan { get; }
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }
    }

    public interface IAnimation 
    {
        Animator playerAnimator { get; }
        void PlayRun();
        void PlayStand();
    }
}
