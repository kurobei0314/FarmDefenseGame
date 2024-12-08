using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace WolfVillage.Common
{
    [RequireComponent(typeof(LoopScrollRect))]
    [DisallowMultipleComponent]
    public class ScrollList<View, ViewModel> : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource 
                                    where View : ScrollPanel<ViewModel>
                                    where ViewModel : ScrollPanelVM
    {
        [SerializeField] private GameObject _prefab;
        private ObjectPool<GameObject> _pool;
        private IReadOnlyList<ViewModel> _dataList;
        private Action<ViewModel> _clickAction;

        public void Initialize(IReadOnlyList<ViewModel> dataList, Action<ViewModel> clickAction)
        {
            _dataList = dataList;
            _clickAction = clickAction;
            InitializeObjectPool();
            // var scrollRect = GetComponent<LoopScrollRect>();
            // scrollRect.prefabSource = this;
            // scrollRect.dataSource = this;
            // scrollRect.RefillCells();
        }

        private void InitializeObjectPool()
        {
            _pool = new ObjectPool<GameObject>(
                () => Instantiate(_prefab),
                o => o.SetActive(true),
                o =>o.SetActive(false));
        }

        void LoopScrollDataSource.ProvideData(Transform trans, int index)
        {
            if (trans.gameObject.TryGetComponent<View>(out var view))
            {
                view.Setup(_dataList[index], (viewModel) => _clickAction(viewModel));
            }
        }

        GameObject LoopScrollPrefabSource.GetObject(int index)
            => _pool.Get();

        void LoopScrollPrefabSource.ReturnObject(Transform trans)
            => _pool.Release(trans.gameObject);
    }
}