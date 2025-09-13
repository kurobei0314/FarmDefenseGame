using WolfVillage.Search.Interface;
using UnityEngine;
using R3;
using WolfVillage.Common;

namespace WolfVillage.Search
{
    public class MoveController : MonoBehaviour
    {
        private IMoveUseCase _moveUseCase;
        public MoveController(ISearchCameraView camera)
        {
            _moveUseCase = new MoveActor(camera);
        }

        public void Initialize(ISearchCameraView camera, IInputController inputController)
        {
            _moveUseCase = new MoveActor(camera);
            Observable.EveryUpdate()
                      .Where(_ => inputController.IsPressed(ActionMapName.SearchMap, SearchGameInputActionName.PlayerMove))
                      .Subscribe(_ => {
                        _moveUseCase.Move(inputController.GetReadValueByVector2(SearchGameInputActionName.PlayerMove));
            }).AddTo(this);
        }
    }
}
