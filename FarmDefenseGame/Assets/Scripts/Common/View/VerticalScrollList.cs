using UnityEngine;

namespace WolfVillage.Common
{
    public class VerticalScrollList<View, ViewModel> : ScrollList<View, ViewModel>
                                    where View : ScrollPanel<ViewModel>
                                    where ViewModel : ScrollPanelVM
    {
        public override void UpdateFocusIndex(Vector2 inputAxis)
        {
            var delta = IsUpInput(inputAxis) ? -1 : 1;

            if (_selectDataIndex + delta < 0 || _selectDataIndex + delta >= _dataList.Count) return;
            FindByView(_dataList[_selectDataIndex]).OnUnFocus();
            _selectDataIndex += delta;
            var afterView = FindByView(_dataList[_selectDataIndex]);
            if (afterView != null)
            { 
                afterView.OnFocus();
                return;
            }
            if      (IsUpInput(inputAxis)) _scrollRect.RefillCellsFromEnd(_selectDataIndex);
            else if (IsDownInput(inputAxis)) _scrollRect.RefillCells(_selectDataIndex);
        }

        private bool IsUpInput(Vector2 inputAxis)
            => inputAxis.y > 0;
        
        private bool IsDownInput(Vector2 inputAxis)
            => inputAxis.y < 0;
    }
}
