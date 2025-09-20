using UnityEngine;

namespace WolfVillage.Search.Interface
{
    public interface ISearchMapView 
    {
        void GetBgLeftDownWorldPosition(ref Vector3 viewPosition);
        void GetBgRightUpWorldPosition(ref Vector3 viewPosition);
    }
}
