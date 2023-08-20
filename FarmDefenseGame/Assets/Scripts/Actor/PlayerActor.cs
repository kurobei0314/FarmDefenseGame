using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerActor : IPlayerUseCase
    {
        IPlayerView playerView;
        public IPlayerView PlayerView => playerView;

        IPlayerEntity playerEntity;
        public IPlayerEntity PlayerEntity => playerEntity;

        ICameraView camera;
        public ICameraView Camera => camera;

        public PlayerActor(IPlayerView playerView, IPlayerEntity playerEntity, ICameraView camera)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
            this.camera = camera;
        }

        public void MovePlayer(float horizontalInput, float verticalInput)
        {
            if (playerEntity.CurrentStatus != Status.IDLE) return;
            var pos = playerView.GameObject.transform.position;
            var moveDirection = (camera.GameObject.transform.forward * verticalInput + camera.GameObject.transform.right * horizontalInput).normalized;
            playerView.Rigidbody.MovePosition(pos + moveDirection * 0.07f);
            playerView.unityChan.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            playerView.PlayRun();
        }
    }
}
