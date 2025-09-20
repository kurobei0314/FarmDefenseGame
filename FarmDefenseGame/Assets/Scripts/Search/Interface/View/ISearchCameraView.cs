using UnityEngine;
namespace WolfVillage.Search
{
    public interface ISearchCameraView
    {
        void AddPosition(Vector3 delta);
        void GetViewportPosition(ref Vector3 worldPosition);
    }
}
