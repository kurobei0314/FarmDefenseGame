using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyAnimationView : MonoBehaviour, IAnimation, IEnemyAnimation
    {
        [SerializeField] private Animator animator;
        public Animator Animator => animator;

        public void PlayWalkAnim()
        {
            animator.Play("WalkFWD");
        }

        public void PlayStandAnim()
        {
            throw new System.NotImplementedException();
        }

        public void PlayAttackAnim()
        {
            animator.Play("Attack02");
        }

        public void PlayNoticeAnim()
        {
            animator.Play("Victory");
        }

        public void PlayOverlookAnim()
        {
            animator.Play("SenseSomethingRPT");
        }
    }
}

