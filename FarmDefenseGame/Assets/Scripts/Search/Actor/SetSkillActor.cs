using System.Linq;
using WolfVillage.Entity.Interface;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SetSkillActor : ISetSkillUseCase
    {
        private ISearchPlayerEntity _playerEntity;
        private ISkillEntity[] _skillEntities;

        public SetSkillActor(   ISearchPlayerEntity playerEntity,
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
    }
}
