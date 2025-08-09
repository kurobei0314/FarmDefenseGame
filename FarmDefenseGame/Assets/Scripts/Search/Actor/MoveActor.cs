using WolfVillage.Search.Interface;
using UnityEngine;

namespace WolfVillage.Search
{
    public class MoveActor : IMoveUseCase
    {
        private ISearchCameraView _cameraView;

        public MoveActor(ISearchCameraView cameraView)
        {
            _cameraView = cameraView;
        }

        public void Move(Vector2 axis)
        {
            _cameraView.AddPosition(axis);
        }
    }
}
