using System.Collections;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class SkillEntity : ISkillEntity
    {
        public enum Status
        {
            CanUse,
            IntervalTime,
        }

        public SkillEntity(ISkillVO skillVO)
        {
            this.skillVO = skillVO;
            current_status = Status.CanUse;
        }

        private ISkillVO skillVO;
        public ISkillVO SkillVO => skillVO;
        private Status current_status;

        // private IEnumerator UpdateStatusForUseSkill()
        // {
        //     UpdateStatus(SkillStatus.IntervalTime);
        //     yield return new WaitForSeconds(SkillVO.IntervalTime);
        //     UpdateStatus(SkillStatus.CanUse);
        // }

        public bool AbleUseSkill()
            => current_status == Status.CanUse;
        
        public void UpdateStatus(Status status)
            => current_status = status;

    }
}
