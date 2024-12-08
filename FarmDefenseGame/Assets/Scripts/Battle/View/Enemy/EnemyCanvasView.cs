
using UnityEngine;
using UnityEngine.UI;

namespace WolfVillage.Battle
{
    public class EnemyCanvasView : MonoBehaviour
    {
        [SerializeField] Image targetLockImage = null;
        [SerializeField] HPBarView hpBarView = null;

        public void Initialize()
        {
            hpBarView.SetValue(1);
            targetLockImage.gameObject.SetActive(false);
        }

        public void SetTargetLockActive(bool active)
        {
            targetLockImage.gameObject.SetActive(active);
        }

        public void SetHPBarValue (float value)
        {
            hpBarView.SetValue(value);
        }

        void LateUpdate()
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
