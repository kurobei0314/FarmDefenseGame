using UnityEngine;
using UnityEngine.InputSystem;

namespace WolfVillage.Search.PlayerMenuUI
{
    public abstract class PlayerMenuElementUI : MonoBehaviour
    {
        protected abstract void ShowUI();
        protected abstract void InputStickEvent(Vector2 axis);
        protected abstract void InputDecideEvent();
        protected abstract void InputBackEvent();
        protected abstract void InputSwitchSubCategoryEvent(int index);
        public abstract void Dispose();

        public void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
            if (active) ShowUI();
        }

        public void InputStick(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var value = context.ReadValue<Vector2>();
            InputStickEvent(value);
        }

        public void InputDecide(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            InputDecideEvent();
        }

        public void InputBack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            InputBackEvent();
        }

        public void InputSwitchSubCategory(InputAction.CallbackContext context, int index)
        {
            if (!context.performed) return;
            InputSwitchSubCategoryEvent(index);
        }
    }
}
