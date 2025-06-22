using System.Collections.Generic;
using WolfVillage.Entity.Interface;
using WolfVillage.Interface;
using WolfVillage.ValueObject.Interface;

namespace WolfVillage.Entity
{
    public class SearchPlayerEntity : PlayerEntity, ISetEquipmentEntity, ISetSkillEntity
    {
        public SearchPlayerEntity ( IPlayerStatusVO playerStatusVO,
                                    Dictionary<RoleType, ISkillEntity[]> skillEntities,
                                    IWeaponEntity weaponEntity,
                                    IArmorEntity armorEntity) : base( playerStatusVO, skillEntities, weaponEntity, armorEntity)
        {
            
        }

        public void SetCurrentWeapon(IWeaponEntity weaponEntity)
            => setCurrentWeapon = weaponEntity;

        public void SetCurrentArmor(IArmorEntity armorEntity)
            => setCurrentArmor = armorEntity;

        public void SetCurrentSkill(ISkillEntity skillEntity, int index)
        {
            var currentSkills = setCurrentSkills[skillEntity.SkillVO.RoleType];
            UnityEngine.Debug.Log("--- スキル切り替え ---");
            UnityEngine.Debug.Log("before " + currentSkills[index]?.SkillVO?.Name ?? "null");
            currentSkills[index] = skillEntity;
            UnityEngine.Debug.Log("after " + currentSkills[index]?.SkillVO?.Name ?? "null");
            UnityEngine.Debug.Log("------");
        }
    }
}
