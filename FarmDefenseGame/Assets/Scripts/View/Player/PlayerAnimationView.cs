using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerAnimationView : MonoBehaviour, IAnimation
    {
        [SerializeField] private Animator animator;
        public Animator Animator => animator;

        public void Walk()
            => animator.Play("Walking(loop)");

        public void Run()
            => animator.Play("Running(loop)");

        public void Stand()
            => animator.Play("Standing(loop)");

        public void Attack()
            => animator.Play("KneelDownToUp");
        

        public void Damage()
            => animator.Play("Damaged(loop)");

        public void Die()
            => animator.Play("GoDown");
        
        public void Avoid()
            => animator.Play("Avoid");
        
        public void JustAvoidAttack()
            => animator.Play("JustAvoidAttack");
    }
}
