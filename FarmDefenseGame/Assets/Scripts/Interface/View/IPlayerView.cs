using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface 
{
    public interface IPlayerView
    {
        GameObject unityChan { get; }
        GameObject GameObject { get; }
        GameObject Body { get; }
        Vector3 Position { get; }
        void Walk(Vector3 moveDirection);
        void Run(Vector3 moveDirection);
        void Stand();
        void Attack();
        void Damage();
        void Die();
        void Avoid(Vector3 moveDirection);
        void AttackFromJustAvoid(Vector3 moveDirection);
    }

    public interface IAnimation 
    {
        Animator Animator { get; }
        void Run();
        void Stand();
        void Attack();
    }

    public interface ISound
    {
        void PlayAttackSound();
    }
}
