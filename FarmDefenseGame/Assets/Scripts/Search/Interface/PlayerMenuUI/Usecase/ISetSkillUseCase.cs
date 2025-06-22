using WolfVillage.Entity.Interface;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public interface ISetSkillUseCase
    {
        RoleType SetWeaponRoleType { get; }
        ISkillEntity[] GetCurrentSkillEntitiesByRoleType(RoleType type);
        ISkillEntity[] GetHasSkillEntitiesByRoleType(RoleType type);
        void SetCurrentSkill(ISkillEntity skill, int index);
    }
}
