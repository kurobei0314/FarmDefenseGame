using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerAnimationView : MonoBehaviour, IAnimation
    {
        [SerializeField] private Animator animator;
        public Animator Animator => animator;

        public void Walk()
        {
            animator.Play("Running(loop)");
        }

        public void Stand()
        {
            animator.Play("Standing(loop)");
        }

        public void Attack()
        {
            animator.Play("KneelDownToUp");
        }
    }
}
