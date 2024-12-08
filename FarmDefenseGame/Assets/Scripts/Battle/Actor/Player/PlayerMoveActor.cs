using WolfVillageBattle.Interface;
using WolfVillage.Entity.Interface;

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

        public void RunPlayer(float horizontalInput, float verticalInput)
        {
            if (playerEntity.CurrentStatus != Status.Idle) return;
            var moveDirection = (camera.CameraTrans.forward * verticalInput + camera.CameraTrans.right * horizontalInput).normalized;
            playerView.Run(moveDirection);
        }

        public void WalkPlayer(float horizontalInput, float verticalInput)
        {
            if (playerEntity.CurrentStatus != Status.Idle) return;
            var moveDirection = (camera.CameraTrans.forward * verticalInput + camera.CameraTrans.right * horizontalInput).normalized;
            playerView.Walk(moveDirection);
        }

        public void StandPlayer()
        {
            if (playerEntity.CurrentStatus != Status.Idle) return;
            playerView.Stand();
        }
    }
}
