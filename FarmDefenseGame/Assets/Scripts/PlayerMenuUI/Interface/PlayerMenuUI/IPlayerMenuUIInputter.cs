using UnityEngine.InputSystem;

namespace WolfVillage.Search.PlayerMenuUI
{
    public interface IPlayerMenuUIInputter
    {
        void SetActive(bool active);
        void InputStickEvent(InputAction.CallbackContext context);
        void InputDecideEvent(InputAction.CallbackContext context);
        void InputCancelEvent(InputAction.CallbackContext context);
        void InputSwitchSubCategoryEvent(InputAction.CallbackContext context, int index);
    }
}
