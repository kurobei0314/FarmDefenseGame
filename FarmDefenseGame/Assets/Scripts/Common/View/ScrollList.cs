using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace WolfVillage.Common
{
    [RequireComponent(typeof(LoopScrollRect))]
    [DisallowMultipleComponent]
    public abstract class ScrollList<View, ViewModel> : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource 
                                    where View : ScrollPanel<ViewModel>
                                    where ViewModel : ScrollPanelVM
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _content;
        private Action<ViewModel> _selectAction;
        protected int _selectDataIndex;
        private ObjectPool<GameObject> _pool;
        protected IReadOnlyList<ViewModel> _dataList;
        protected LoopScrollRect _scrollRect;

        public void Initialize(ViewModel[] dataList, Action<ViewModel> selectAction, int selectedViewModelIndex = 0)
        {
            _dataList = dataList;
            _selectAction = selectAction;
            _selectDataIndex = selectedViewModelIndex;
            InitializeObjectPool();
            _scrollRect = GetComponent<LoopScrollRect>();
            _scrollRect.prefabSource = this;
            _scrollRect.dataSource = this;
            _scrollRect.totalCount = dataList.Length;
            _scrollRect.RefillCells();
        }

        private void InitializeObjectPool()
        {
            _pool = new ObjectPool<GameObject>(
                () => Instantiate(_prefab),
                o => o.SetActive(true),
                o => o.SetActive(false));
        }

        protected View FindByView(ViewModel viewModel)
        {
            for (var i = 0; i < _content.childCount; i++)
            {
                var panel = _content.GetChild(i);
                if (!panel.TryGetComponent<View>(out var view)) continue;
                if (view.Data != viewModel) continue;
                return view;
            }
            return null;
        }

        public abstract void UpdateFocusIndex(Vector2 inputAxis);

        #region LoopScrollDataSource
        public virtual void ProvideData(Transform trans, int index)
        {
            if (trans.gameObject.TryGetComponent<View>(out var view))
            {
                view.Setup(_dataList[index], _selectAction);
                if (index == _selectDataIndex) view.OnFocus();
                else view.OnUnFocus();
            }
        }
        #endregion

        #region LoopScrollPrefabSource
        public GameObject GetObject(int index)
            => _pool.Get();

        void LoopScrollPrefabSource.ReturnObject(Transform trans)
            => _pool.Release(trans.gameObject);
        #endregion
    }
}