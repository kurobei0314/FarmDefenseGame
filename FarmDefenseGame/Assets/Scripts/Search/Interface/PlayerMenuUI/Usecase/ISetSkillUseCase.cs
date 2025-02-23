using WolfVillage.Entity.Interface;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public interface ISetSkillUseCase
    {
        ISkillEntity[] CurrentSkillEntities { get; }
        ISkillEntity[] HasSkillEntities { get; }
        RoleType SetWeaponRoleType { get; } 
    }
}
