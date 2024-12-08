using UnityEngine;
using UnityEngine.UI;

namespace WolfVillage.Battle
{
    public class HPBarView : MonoBehaviour
    {
        [SerializeField] private Image hpBar;

        public void SetValue (float value)
        {
            hpBar.fillAmount = value;
        }
    }
}
