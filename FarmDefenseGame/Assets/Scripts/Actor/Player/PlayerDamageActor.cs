using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class PlayerDamageActor : IPlayerDamageUseCase
    {
        private IPlayerView playerView;
        private IPlayerEntity playerEntity;

        public PlayerDamageActor(IPlayerView playerView, IPlayerEntity playerEntity)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
        }

        public void ReduceHP (int damage)
        {
            playerEntity.ReduceHP(damage);
        }

        public void Damage()
        {
            playerEntity.SetStatus(Status.Damage);
            playerView.Damage((float)playerEntity.CurrentHPValue/playerEntity.PlayerVO.MaxHP);
        }

        public void Die()
        {
            playerEntity.SetStatus(Status.Die);
            playerView.Die();
        }
    }
    
}
