using UnityEngine;

namespace WolfVillage.Search
{
    public class SearchCameraView : MonoBehaviour, ISearchCameraView
    {
        [SerializeField] private Camera _camera;

        public void AddPosition(Vector3 delta)
        {
            _camera.transform.position += delta;
        }
    }
}
