using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField]
        private GameObject unity_chan;
        public GameObject unityChan => unity_chan;

        public GameObject GameObject => this.gameObject;
        public Rigidbody Rigidbody => this.GetComponent<Rigidbody>();

        [SerializeField]
        private Animator animator;
        public Animator Animator => animator;

        public void PlayRun()
        {
            animator.Play("Running(loop)");
        }

        public void PlayStand()
        {
            animator.Play("Standing(loop)");
        }

        public void PlayAttack()
        {
            animator.Play("KneelDownToUp");
        }

        // TODO: これ今はPlayerStatusViewにいるので、それをどうにかしたい
        // public void SetIdleStatus()
        // {
        //     playerEntity.SetStatus(Status.ATTACK);
        // }

        public void PlayAttackSound()
        {
            float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
            if (rnd < 0.5f)
            {
                //AudioManager.Instance.PlaySE("unitychan_attack1");
            } else {
                //AudioManager.Instance.PlaySE("unitychan_attack2");
            }
        }

    }
}
