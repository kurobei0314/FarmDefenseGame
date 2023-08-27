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
        private Animator player_animator;
        public Animator playerAnimator => player_animator;

        public void PlayRun()
        {
            playerAnimator.Play("Running(loop)");
        }

        public void PlayStand()
        {
            playerAnimator.Play("Standing(loop)");
        }
    }
}
