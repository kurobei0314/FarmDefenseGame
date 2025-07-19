using UnityEngine;
using UnityEngine.InputSystem;
using WolfVillage.Search.PlayerMenuUI.EquipmentMenu;
using WolfVillage.Search.PlayerMenuUI.SkillMenu;
using System.Collections.Generic;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class PlayerMenuContentUI : MonoBehaviour
    {
        // キーはPlayerMenuStateのenumをintにキャストしたもの //
        [SerializeField] private List<PlayerMenuElementUI> _playerMenuElements;
        private PlayerMenuElementUI _currentPlayerMenuUI;
        public void Initialize( PlayerMenuState currentPlayerMenuState, 
                                ISetEquipmentUseCase equipmentUseCase,
                                ISetSkillUseCase skillUseCase)
        {
            InitializeElementUIs(equipmentUseCase, skillUseCase);
            UpdateView(currentPlayerMenuState);
        }

        private void InitializeElementUIs(ISetEquipmentUseCase equipmentUseCase,
                                            ISetSkillUseCase skillUseCase)
        {
            var equipmentUI = _playerMenuElements[(int)PlayerMenuState.Equipment] as EquipmentMenuUI;
            equipmentUI.Initialize(equipmentUseCase);
            var skillUI = _playerMenuElements[(int)PlayerMenuState.Skill] as SkillMenuUI;
            skillUI.Initialize(skillUseCase);
        }

        public void UpdateView(PlayerMenuState currentPlayerMenuState)
        {
            // 今開いているUIを非表示にする
            _currentPlayerMenuUI?.SetActive(false);
            SetPlayerMenuUIInputter(currentPlayerMenuState);
            // 開く予定のUIを表示する
            _currentPlayerMenuUI?.SetActive(true);
        }

        private void SetPlayerMenuUIInputter(PlayerMenuState currentPlayerMenuState)
            => _currentPlayerMenuUI = _playerMenuElements[(int)currentPlayerMenuState];

        public void InputStickEvent(InputAction.CallbackContext context)
            => _currentPlayerMenuUI.InputStick(context);

        public void InputDecideEvent(InputAction.CallbackContext context)
            => _currentPlayerMenuUI.InputDecide(context);

        public void InputCancelEvent(InputAction.CallbackContext context)
            => _currentPlayerMenuUI.InputCancel(context);

        public void InputSwitchSubCategoryEvent(InputAction.CallbackContext context, int index)
            => _currentPlayerMenuUI.InputSwitchSubCategory(context, index);

        public void Dispose()
        {
            for (var i = 0; i < _playerMenuElements.Count; i++)
            {
                _playerMenuElements[i].Dispose();
            }
        }
    }
}
