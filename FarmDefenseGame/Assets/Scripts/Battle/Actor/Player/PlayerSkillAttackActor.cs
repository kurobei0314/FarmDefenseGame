using WolfVillage.Entity;
using WolfVillage.Battle.Interface;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle
{
    public class PlayerSkillAttackActor : IPlayerSkillAttackUseCase
    {
        private IPlayerView playerView;
        private IBattlePlayerEntity playerEntity;
        private IInGameView gameView;

        public PlayerSkillAttackActor(IPlayerView playerView, IBattlePlayerEntity playerEntity, IInGameView gameView)
        {
            this.playerView = playerView;
            this.playerEntity = playerEntity;
            this.gameView = gameView;
        }

        public void AttackPlayer(int index)
        {
            var skillEntity = playerEntity.SetCurrentSkills[index];
            if (skillEntity == null) return; 
            if (!skillEntity.AbleUseSkill()) return;
            
            gameView.UpdateSkillIconViewForUseSkill(index);
            skillEntity.UpdateStatus(SkillEntity.Status.IntervalTime);
            playerView.SkillAttack();
            playerEntity.SetStatus(Status.Attack);
        }

        public void FinishIntervalTimeSkill(int index)
        {
            var skillEntity = playerEntity.SetCurrentSkills[index];
            if (skillEntity == null) return;

            skillEntity.UpdateStatus(SkillEntity.Status.CanUse);
        }
    }
}
