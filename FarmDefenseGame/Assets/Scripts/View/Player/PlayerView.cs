using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private GameObject unity_chan;
        [SerializeField] private GameObject body;
        [SerializeField] private PlayerAnimationView playerAnimationView;
        [SerializeField] private PlayerSEView playerSEView;
        
        public GameObject unityChan => unity_chan;
        public GameObject Body => body;

        public GameObject GameObject => this.gameObject;
        public Rigidbody Rigidbody => this.GetComponent<Rigidbody>();
        public Vector3 Position => this.gameObject.transform.position;

        public void Walk()
        {
            playerAnimationView.Walk();
        }

        public void Stand()
        {
            playerAnimationView.Stand();
        }

        public void Attack()
        {
            playerAnimationView.Attack();
            playerSEView.PlayAttackSound();
        }

        // TODO: これ今はPlayerStatusViewにいるので、それをどうにかしたい
        // public void SetIdleStatus()
        // {
        //     playerEntity.SetStatus(Status.ATTACK);
        // }

    }
}
