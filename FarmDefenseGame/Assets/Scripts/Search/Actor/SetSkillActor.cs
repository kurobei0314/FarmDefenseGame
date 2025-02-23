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

        public ISkillEntity[] CurrentSkillEntities => _playerEntity.CurrentSkills;
        public ISkillEntity[] HasSkillEntities => _skillEntities;
        public RoleType SetWeaponRoleType => _playerEntity.CurrentWeapon.WeaponVO.RoleType; 
    }
}
