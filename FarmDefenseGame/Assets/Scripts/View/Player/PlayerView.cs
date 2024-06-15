using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        private Rigidbody Rigidbody => this.GetComponent<Rigidbody>();
        public Vector3 Position => this.gameObject.transform.position;

        public void Walk(Vector3 moveDirection)
        {
            var pos = this.gameObject.transform.position;
            MovePlayer(moveDirection, pos + moveDirection * 0.04f);
            playerAnimationView.Walk();
        }

        public void Run(Vector3 moveDirection)
        {
            var pos = this.gameObject.transform.position;
            MovePlayer(moveDirection, pos + moveDirection * 0.07f);
            playerAnimationView.Run();
        }

        private void MovePlayer(Vector3 moveDirection, Vector3 afterPosition)
        {
            Rigidbody.MovePosition(afterPosition);
            unity_chan.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
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

        public void Damage()
        {
            playerAnimationView.Damage();
            playerSEView.PlayDamageSound();
        }

        public void Die()
        {
            playerAnimationView.Die();
            playerSEView.PlayDieSound();
        }

        public void Avoid(Vector3 moveDirection)
        {
            var pos = this.gameObject.transform.position;
            MovePlayerForSeconds(moveDirection, pos + moveDirection * 5.0f, 1.0f);
            playerAnimationView.Avoid();
        }

        public void JustAvoidAttack(Vector3 targetPosition)
        {
            var direction = (targetPosition - unity_chan.transform.position).normalized;
            MovePlayerForSeconds(direction, targetPosition, 0.1f);
            // TODO: 一旦、attackのアニメーションとSEを流す
            playerAnimationView.JustAvoidAttack();
            playerSEView.PlayAttackSound();
        }

        private void MovePlayerForSeconds(Vector3 moveDirection, Vector3 afterPosition, float time)
        {
            Rigidbody.DOMove(afterPosition, time);
            unity_chan.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        }
    }
}
