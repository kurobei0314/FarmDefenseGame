using WolfVillage.Search.Interface;
using UnityEngine;

namespace WolfVillage.Search
{
    public class MoveActor : IMoveUseCase
    {
        private ISearchCameraView _cameraView;
        private ISearchMapView _mapView;

        public MoveActor(ISearchCameraView cameraView, ISearchMapView mapView)
        {
            _cameraView = cameraView;
            _mapView = mapView;
        }

        public void Move(Vector2 axis)
        {
            var leftDownViewportPos = new Vector3();
            var rightUpViewportPos = new Vector3();
            GetViewportPosition(ref leftDownViewportPos, ref rightUpViewportPos);

            var delta = new Vector3();
            if (axis.x != 0) 
                CalculateCameraPositionX(axis.x, leftDownViewportPos, rightUpViewportPos, ref delta);
            if (axis.y != 0) 
                CalculateCameraPositionY(axis.y, leftDownViewportPos, rightUpViewportPos, ref delta);

            _cameraView.AddPosition(delta);
        }

        private void GetViewportPosition(ref Vector3 leftDownViewportPos, ref Vector3 rightUpViewportPos)
        {
            // 背景のWorld座標を求める
            _mapView.GetBgLeftDownWorldPosition(ref leftDownViewportPos);
            _mapView.GetBgRightUpWorldPosition(ref rightUpViewportPos);

            // 背景のViewPort座標を求める
            _cameraView.GetViewportPosition(ref leftDownViewportPos);
            _cameraView.GetViewportPosition(ref rightUpViewportPos);
        }

        private void CalculateCameraPositionX(  float deltaX,
                                                in Vector3 leftDownPos,
                                                in Vector3 rightUpPos,
                                                ref Vector3 delta)
        {
            if      (deltaX < 0 && leftDownPos.x >= 0) return;
            else if (deltaX > 0 && rightUpPos.x  <= 1) return;
            delta.x = deltaX;
        }

        private void CalculateCameraPositionY(  float deltaY,
                                                in Vector3 leftDownPos,
                                                in Vector3 rightUpPos,
                                                ref Vector3 delta)
        {
            if      (deltaY < 0 && leftDownPos.y >= 0) return;
            else if (deltaY > 0 && leftDownPos.y <= 1) return;
            delta.y = deltaY;
        }
    }
}
