using System.Linq;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class SetEquipmentActor : ISetEquipmentUseCase
    {
        private ISearchPlayerEntity _playerEntity;
        private IWeaponEntity[] _hasWeaponEntity;
        private IArmorEntity[] _hasArmorEntity;

        public IWeaponEntity PlayerCurrentWeapon => _playerEntity.CurrentWeapon;
        public IArmorEntity PlayerCurrentArmor => _playerEntity.CurrentArmor;
        public IWeaponEntity[] HasWeaponEntity => _hasWeaponEntity;
        public IArmorEntity[] HasArmorEntity => _hasArmorEntity;

        public SetEquipmentActor(ISearchPlayerEntity playerEntity,
                                IWeaponEntity[] hasWeaponEntity,
                                IArmorEntity[] hasArmorEntity)
        {
            _playerEntity = playerEntity;
            _hasWeaponEntity = hasWeaponEntity;
            _hasArmorEntity = hasArmorEntity;
        }

        public IWeaponEntity GetWeaponEntityById(int weaponEntityId)
            =>  _hasWeaponEntity.FirstOrDefault(entity => entity.Id == weaponEntityId);

        public IArmorEntity GetArmorEntityById(int armorEntityId)
            => _hasArmorEntity.FirstOrDefault(entity => entity.Id == armorEntityId);

        public void SetCurrentWeapon(int weaponEntityId)
        {
            var entity = GetWeaponEntityById(weaponEntityId);
            _playerEntity.SetCurrentWeapon(entity);
        }

        public void SetCurrentArmor(int armorEntityId)
        {
            var entity = GetArmorEntityById(armorEntityId);
            _playerEntity.SetCurrentArmor(entity);   
        }
    }
}
