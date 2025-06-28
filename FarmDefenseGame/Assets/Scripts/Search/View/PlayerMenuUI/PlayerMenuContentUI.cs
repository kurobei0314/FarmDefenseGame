using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Search.PlayerMenuUI.EquipmentMenu;
using WolfVillage.Search.PlayerMenuUI.SkillMenu;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class PlayerMenuContentUI : MonoBehaviour
    {
        [SerializeField] private EquipmentMenuUI _equipmentMenuUI;
        [SerializeField] private SkillMenuUI _skillMenuUI;
        private IPlayerMenuUIInputter _currentPlayerMenuUI;
        public void Initialize( PlayerMenuState currentPlayerMenuState, 
                                ISetEquipmentUseCase equipmentUseCase,
                                ISetSkillUseCase skillUseCase)
        {
            _equipmentMenuUI.Initialize(equipmentUseCase);
            _skillMenuUI.Initialize(skillUseCase);
            SetPlayerMenuUIInputter(currentPlayerMenuState);
        }

        private void SetPlayerMenuUIInputter(PlayerMenuState currentPlayerMenuState)
            => _currentPlayerMenuUI = GetPlayerMenuUIInputter(currentPlayerMenuState);

        private IPlayerMenuUIInputter GetPlayerMenuUIInputter(PlayerMenuState currentPlayerMenuState)
        => currentPlayerMenuState switch
        {
                PlayerMenuState.Status => null,
                PlayerMenuState.Equipment => _equipmentMenuUI,
                PlayerMenuState.Skill => _skillMenuUI,
                PlayerMenuState.Inventory => null,
                PlayerMenuState.Setting => null,
                _ => null
        };

        public void InputStickEvent(InputAction.CallbackContext context)
            => _currentPlayerMenuUI.InputStickEvent(context);

        public void InputDecideEvent(InputAction.CallbackContext context)
            => _currentPlayerMenuUI.InputDecideEvent(context);

        public void InputCancelEvent(InputAction.CallbackContext context)
            => _currentPlayerMenuUI.InputCancelEvent(context);

        public void InputSwitchSubCategoryEvent(InputAction.CallbackContext context)
            => _currentPlayerMenuUI.InputSwitchSubCategoryEvent(context);

        public void Dispose()
        {
            _equipmentMenuUI.Dispose();
            _skillMenuUI.Dispose();
        }
    }
}
