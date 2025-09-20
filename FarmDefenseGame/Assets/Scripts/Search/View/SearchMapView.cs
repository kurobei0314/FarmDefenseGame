using UnityEngine;
using WolfVillage.Search.Interface;

namespace WolfVillage.Search
{
    public class SearchMapView : MonoBehaviour, ISearchMapView
    {
        [SerializeField] private SpriteRenderer _bg;

        public void GetBgLeftDownWorldPosition(ref Vector3 viewPosition)
        {
            viewPosition = new Vector3 (_bg.transform.position.x - _bg.bounds.size.x / 2,
                                        _bg.transform.position.y - _bg.bounds.size.y / 2,
                                        0);
        }

        public void GetBgRightUpWorldPosition(ref Vector3 viewPosition)
        { 
            viewPosition = new Vector3 (_bg.transform.position.x + _bg.bounds.size.x / 2,
                                        _bg.transform.position.y + _bg.bounds.size.y / 2,
                                        0);
        }
    }
}
