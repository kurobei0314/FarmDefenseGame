using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemyAnimationView : MonoBehaviour, IAnimation, IEnemyAnimation
    {
        [SerializeField] private Animator animator;
        public Animator Animator => animator;

        public void Walk()
        {
            animator.Play("WalkFWD");
        }

        public void Stand()
        {
            throw new System.NotImplementedException();
        }

        public void Attack()
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
        
        public void PlayDamageAnim()
        {
            animator.Play("GetHit");
        }

        public void PlayDieAnim()
        {
            animator.Play("Die");
        }
    }
}

