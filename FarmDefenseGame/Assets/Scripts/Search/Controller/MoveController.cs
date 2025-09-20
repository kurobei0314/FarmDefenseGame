using WolfVillage.Search.Interface;
using UnityEngine;
using R3;
using WolfVillage.Common;

namespace WolfVillage.Search
{
    public class MoveController : MonoBehaviour
    {
        private IMoveUseCase _moveUseCase;
        public MoveController(ISearchCameraView camera, ISearchMapView map)
        {
            _moveUseCase = new MoveActor(camera, map);
        }

        public void Initialize(ISearchCameraView camera, ISearchMapView map, IInputController inputController)
        {
            _moveUseCase = new MoveActor(camera, map);
            Observable.EveryUpdate()
                      .Where(_ => inputController.IsPressed(ActionMapName.SearchMap, SearchGameInputActionName.PlayerMove))
                      .Subscribe(_ => {
                        _moveUseCase.Move(inputController.GetReadValueByVector2(SearchGameInputActionName.PlayerMove));
            }).AddTo(this);
        }
    }
}
