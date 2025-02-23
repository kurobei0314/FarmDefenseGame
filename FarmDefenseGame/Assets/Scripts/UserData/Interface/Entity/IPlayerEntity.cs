using System.Collections.Generic;
using WolfVillage.Interface;

namespace WolfVillage.Entity.Interface
{
    public interface IPlayerEntity
    {
        int CurrentMaxHP { get; }
        IWeaponEntity CurrentWeapon { get; }
        IArmorEntity CurrentArmor { get; }
        Dictionary<RoleType, ISkillEntity[]> CurrentAllRoleTypeSkills { get; }
    }
}
