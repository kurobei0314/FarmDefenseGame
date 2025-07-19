using UnityEngine;
using UnityEngine.InputSystem;

namespace WolfVillage.Search.PlayerMenuUI
{
    public abstract class PlayerMenuElementUI : MonoBehaviour
    {
        protected abstract void InputStickEvent(Vector2 axis);
        protected abstract void InputDecideEvent();
        protected abstract void InputCancelEvent();
        protected abstract void InputSwitchSubCategoryEvent(int index);
        public abstract void Dispose();

        public void SetActive(bool active)
            => this.gameObject.SetActive(active);

        public void InputStick(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            var value = context.ReadValue<Vector2>();
            InputStickEvent(value);
        }

        public void InputDecide(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            InputDecideEvent();
        }

        public void InputCancel(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            InputCancelEvent();
        }

        public void InputSwitchSubCategory(InputAction.CallbackContext context, int index)
        {
            if (!context.performed) return;
            InputSwitchSubCategoryEvent(index);
        }
    }
}
