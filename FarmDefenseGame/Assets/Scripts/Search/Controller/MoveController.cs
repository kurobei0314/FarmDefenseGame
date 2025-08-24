using UnityEngine.InputSystem;
using WolfVillage.Search.Interface;
using UnityEngine;
using R3;

namespace WolfVillage.Search
{
    public class MoveController : MonoBehaviour
    {
        private IMoveUseCase _moveUseCase;
        public MoveController(ISearchCameraView camera)
        {
            _moveUseCase = new MoveActor(camera);
        }

        public void Initialize(ISearchCameraView camera)
        {
            //Debug.Log("wa-----i");
            _moveUseCase = new MoveActor(camera);
            // Observable.EveryUpdate().Where(_ => playerInput.actions["Move"].IsPressed()).Subscribe(_ => {
            //     //Debug.Log("search wa-----i");
            // }).AddTo(this);
        }

        public void Input(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var value = context.ReadValue<Vector2>();
            _moveUseCase.Move(value);
        }
    }
}
