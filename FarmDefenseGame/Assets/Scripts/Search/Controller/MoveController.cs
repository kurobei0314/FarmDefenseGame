using UnityEngine.InputSystem;
using WolfVillage.Search.Interface;
using UnityEngine;

namespace WolfVillage.Search
{
    public class MoveController
    {
        private IMoveUseCase _moveUseCase;
        public MoveController(ISearchCameraView camera)
        {
            _moveUseCase = new MoveActor(camera);
        }

        public void Input(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            var value = context.ReadValue<Vector2>();
            _moveUseCase.Move(value);
        }
    }
}
