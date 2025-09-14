using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Search.PlayerMenuUI.EquipmentMenu;
using WolfVillage.Search.PlayerMenuUI.SkillMenu;
using Extension;
using WolfVillage.Entity.Interface;
using System;

namespace WolfVillage.Search.PlayerMenuUI
{
    public interface IPlayerMenuUI
    {
        public void Open( IPlayerEntity player,
                          IWeaponEntity[] weaponEntities,
                          IArmorEntity[] armorEntities,
                          ISkillEntity[] skillEntities,
                          Action closeAction);
    }

    public class PlayerMenuUI : MonoBehaviour, IPlayerMenuUI
    {
        [SerializeField] private PlayerMenuHeaderUI _headerUI;
        [SerializeField] private PlayerMenuContentUI _contentUI;

        private PlayerMenuUIVM _playerMenuVM;
        private Action _closeAction;

        public void Open( IPlayerEntity player,
                          IWeaponEntity[] weaponEntities,
                          IArmorEntity[] armorEntities,
                          ISkillEntity[] skillEntities,
                          Action closeAction)
        {
            this.gameObject.SetActive(true);

            // TODO: ここの処理も別の場所に書く
            var equipmentActor = new SetEquipmentActor((ISetEquipmentEntity)player, weaponEntities, armorEntities);
            var skillActor = new SetSkillActor((ISetSkillEntity)player, skillEntities);

            _playerMenuVM = new PlayerMenuUIVM();
            _contentUI.Initialize(_playerMenuVM.State, equipmentActor, skillActor);
            _headerUI.UpdateView(_playerMenuVM.State);

            _closeAction = closeAction;
        }

        #region InputSystemEventHandler
        // TODO: 絶対にUI全体を管理するクラスを作成し、そこで通知を受け取るようにする
        public void InputStickEvent(InputAction.CallbackContext context)
            => _contentUI.InputStickEvent(context);
        public void InputDecideEvent(InputAction.CallbackContext context)
            => _contentUI.InputDecideEvent(context);
        public void InputCancelEvent(InputAction.CallbackContext context)
            => _contentUI.InputCancelEvent(context);
        public void InputSwitchPreCategoryEvent(InputAction.CallbackContext context)
            => UpdatePlayerMenuContentView(context, -1);
        public void InputSwitchNextCategoryEvent(InputAction.CallbackContext context)
            => UpdatePlayerMenuContentView(context, 1);
        public void InputSwitchPreSubCategoryEvent(InputAction.CallbackContext context)
            => _contentUI.InputSwitchSubCategoryEvent(context, -1);
        public void InputSwitchNextSubCategoryEvent(InputAction.CallbackContext context)
            => _contentUI.InputSwitchSubCategoryEvent(context, 1);
        #endregion

        private void UpdatePlayerMenuContentView(InputAction.CallbackContext context, int index)
        {
            if (!context.started) return;
            var nextIndex = (int)_playerMenuVM.State + index;
            if (nextIndex < EnumHelper.GetMinIndexEnum<PlayerMenuState>())
                nextIndex = EnumHelper.GetMaxIndexEnum<PlayerMenuState>();
            else if (EnumHelper.GetMaxIndexEnum<PlayerMenuState>() < nextIndex)
                nextIndex = EnumHelper.GetMinIndexEnum<PlayerMenuState>();
            
            _playerMenuVM.SetPlayerMenuState((PlayerMenuState)nextIndex);
            _contentUI.UpdateView(_playerMenuVM.State);
            _headerUI.UpdateView(_playerMenuVM.State);
        }

        public void Dispose()
        {
            _headerUI.Dispose();
            _contentUI.Dispose();
        }
    }
}
