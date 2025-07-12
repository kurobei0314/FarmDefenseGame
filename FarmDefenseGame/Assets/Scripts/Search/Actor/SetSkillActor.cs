using System.Linq;
using WolfVillage.Entity.Interface;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SetSkillActor : ISetSkillUseCase
    {
        private ISetSkillEntity _playerEntity;
        private ISkillEntity[] _skillEntities;

        public SetSkillActor(   ISetSkillEntity playerEntity,
                                ISkillEntity[] skillEntities)
        {
            _playerEntity = playerEntity;
            _skillEntities = skillEntities;
        }

        public ISkillEntity[] GetCurrentSkillEntitiesByRoleType(RoleType type)
            => _playerEntity.CurrentAllRoleTypeSkills[type];
        public ISkillEntity[] GetHasSkillEntitiesByRoleType(RoleType type)
            => _skillEntities.Where(entity => entity.SkillVO.RoleType == type).ToArray();
        public RoleType SetWeaponRoleType => _playerEntity.CurrentWeapon.WeaponVO.RoleType;

        public void UpdateCurrentSkill(ISkillEntity skill, int index)
        {
            var setSkills = GetCurrentSkillEntitiesByRoleType(skill.SkillVO.RoleType);
            for (var i = 0; i < setSkills.Count(); i++)
            {
                if (setSkills[i]?.Id == skill.Id)
                {
                    // セットしようとしたところに装備されてたら外す //
                    if (i == index) UnSetCurrentSkill(skill.SkillVO.RoleType, index);
                    // 別のところにセットされていたら交換する //
                    else ExchangeSkill(i, index, skill.SkillVO.RoleType);
                    return;
                }
            }
            _playerEntity.SetCurrentSkill(skill, skill.SkillVO.RoleType, index);
        }

        private void UnSetCurrentSkill(RoleType type, int index)
            => _playerEntity.SetCurrentSkill(null, type, index);
        
        private void ExchangeSkill(int skillIndex1, int skillIndex2, RoleType type)
        {
            var setSkills = GetCurrentSkillEntitiesByRoleType(type);
            var skill1 = setSkills[skillIndex1];
            _playerEntity.SetCurrentSkill(setSkills[skillIndex2], type, skillIndex1);
            _playerEntity.SetCurrentSkill(skill1, type, skillIndex2);
        }
    }
}
