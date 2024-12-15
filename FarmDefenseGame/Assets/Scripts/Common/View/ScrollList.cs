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
        private Action<ViewModel> _clickAction;
        private int _selectDataIndex;
        private ObjectPool<GameObject> _pool;
        protected IReadOnlyList<ViewModel> _dataList;

        public void Initialize(ViewModel[] dataList, Action<ViewModel> clickAction)
        {
            _dataList = dataList;
            _clickAction = clickAction;
            InitializeObjectPool();
            var scrollRect = GetComponent<LoopScrollRect>();
            scrollRect.prefabSource = this;
            scrollRect.dataSource = this;
            scrollRect.totalCount = dataList.Length;
            scrollRect.RefillCells();
        }

        private void InitializeObjectPool()
        {
            _pool = new ObjectPool<GameObject>(
                () => Instantiate(_prefab),
                o => o.SetActive(true),
                o =>o.SetActive(false));
        }

        #region LoopScrollDataSource
        public virtual void ProvideData(Transform trans, int index)
        {
            if (trans.gameObject.TryGetComponent<View>(out var view))
            {
                view.Setup(_dataList[index], _clickAction);
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