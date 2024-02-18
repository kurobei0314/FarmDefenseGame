using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerMoveActor : IPlayerMoveUseCase
    {
        private IPlayerView playerView;
        private IPlayerEntity playerEntity;
        private ICameraView camera;

        public PlayerMoveActor(IPlayerView playerView, IPlayerEntity playerEntity, ICameraView camera)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
            this.camera = camera;
        }

        public void MovePlayer(float horizontalInput, float verticalInput)
        {
            if (playerEntity.CurrentStatus != Status.IDLE) return;
            var pos = playerView.GameObject.transform.position;
            var moveDirection = (camera.CameraTrans.forward * verticalInput + camera.CameraTrans.right * horizontalInput).normalized;
            playerView.Rigidbody.MovePosition(pos + moveDirection * 0.07f);
            playerView.unityChan.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            playerView.Walk();
        }
        public void StandPlayer()
        {
            if (playerEntity.CurrentStatus != Status.IDLE) return;
            playerView.Stand();
        }
    }
}
