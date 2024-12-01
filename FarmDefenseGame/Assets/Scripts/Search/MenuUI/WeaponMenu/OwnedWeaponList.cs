using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

[RequireComponent(typeof(LoopScrollRect))]
[DisallowMultipleComponent]
public class OwnedWeaponList : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource
{
    [SerializeField] private GameObject _prefab;
    private ObjectPool<GameObject> _pool;

    public void Initialize()
    {
        _pool = new ObjectPool<GameObject>(
            () => Instantiate(_prefab),
            o => o.SetActive(true),
            o =>o.SetActive(false));

        var scrollRect = GetComponent<LoopScrollRect>();
        scrollRect.prefabSource = this;
        scrollRect.dataSource = this;
        scrollRect.RefillCells();
    }

    void LoopScrollDataSource.ProvideData(Transform trans, int index)
    {
        // データのインデックスを求める
        // var dataIndex = (int)Mathf.Repeat(index, dataCount);
        // trans.GetChild(0).GetComponent<TextMeshProUGUI>().text = index.ToString();
    }

    GameObject LoopScrollPrefabSource.GetObject(int index)
        => _pool.Get();

    void LoopScrollPrefabSource.ReturnObject(Transform trans)
        => _pool.Release(trans.gameObject);
}
