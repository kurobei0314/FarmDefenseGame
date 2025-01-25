using UnityEngine;
using UnityEngine.UI;

namespace WolfVillage.Common
{
    public class VerticalScrollList<View, ViewModel> : ScrollList<View, ViewModel>
                                    where View : ScrollPanel<ViewModel>
                                    where ViewModel : ScrollPanelVM
    {
        private float _space => _content.GetComponent<VerticalLayoutGroup>()?.spacing ?? 0.0f;
        private float cellHeight => _prefab.GetComponent<RectTransform>()?.rect.height ?? 0.0f;

        protected override bool IsViewportSizeAppropriate()
        {
            _content.TryGetComponent<VerticalLayoutGroup>(out var layoutGroup);
            if (layoutGroup == null)
            {
                Debug.LogError("contentにVerticalLayoutGroupが設定されていません");
                return false;
            }
            var remainingHeight = viewportHeight % (cellHeight + _space);
            if (viewportHeight % (cellHeight + _space) == 0) return true;
            return false;
        }

        public override void UpdateFocusIndex(Vector2 inputAxis)
        {
            if (IsHorizonInput(inputAxis)) return;
            var delta = IsUpInput(inputAxis) ? -1 : 1;

            if (_selectDataIndex + delta < 0 || _selectDataIndex + delta >= _dataList.Count) return;
            FindByView(_dataList[_selectDataIndex]).OnUnFocus();
            _selectDataIndex += delta;
            var afterView = FindByView(_dataList[_selectDataIndex]);
            if (afterView != null && IsInViewportCell(afterView))
            { 
                afterView.OnFocus();
                return;
            }
            if      (IsUpInput(inputAxis))   _scrollRect.RefillCells(_selectDataIndex);
            else if (IsDownInput(inputAxis)) _scrollRect.RefillCells(GetHeadScrollIndexFromEndIndex(_selectDataIndex));
        }

        private bool IsInViewportCell(View cell)
        {
            var cellUpY = cell.GetComponent<Transform>().localPosition.y;
            var cellDownY = cellUpY - cellHeight - _space;

            if (0 >= cellUpY && cellDownY >= -viewportHeight) return true;
            return false;
        }
        private int GetHeadScrollIndexFromEndIndex(int _endDataIndex)
        {
            var num = (int) (viewportHeight / (cellHeight + _space));
            return _endDataIndex + 1 - num;
        }

        private bool IsHorizonInput(Vector2 inputAxis)
            => inputAxis.x != 0;

        private bool IsUpInput(Vector2 inputAxis) 
            => inputAxis.y > 0;
        
        private bool IsDownInput(Vector2 inputAxis)
            => inputAxis.y < 0;
    }
}
