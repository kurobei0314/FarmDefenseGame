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
        [SerializeField] private HPBarView hPBarView;
        
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

        public void Damage(float currentHP)
        {
            playerAnimationView.Damage();
            playerSEView.PlayDamageSound();
            hPBarView.SetValue(currentHP);
        }

        public void Die()
        {
            playerAnimationView.Die();
            playerSEView.PlayDieSound();
            hPBarView.SetValue(0);
        }

        // TODO: これ今はPlayerStatusViewにいるので、それをどうにかしたい
        // public void SetIdleStatus()
        // {
        //     playerEntity.SetStatus(Status.ATTACK);
        // }

    }
}
