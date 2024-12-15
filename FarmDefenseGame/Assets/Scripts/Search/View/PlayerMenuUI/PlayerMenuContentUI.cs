using UnityEngine;
using WolfVillage.Entity;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class PlayerMenuContentUI : MonoBehaviour
    {
        [SerializeField] private WeaponMenuUI _weaponMenuUI;
        public void Initialize(IPlayerEntity playerEntity, IWeaponEntity[] weaponEntities, IArmorEntity[] armorEntities)
        {
            _weaponMenuUI.Initialize(playerEntity.SetCurrentWeapon, playerEntity.SetCurrentArmor, weaponEntities, armorEntities);
        }
    }
}
