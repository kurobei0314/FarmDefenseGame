using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WolfVillageBattle
{
    public class HPBarView : MonoBehaviour
    {
        [SerializeField] private Slider hpBar;

        public void SetValue (float value)
        {
            hpBar.value = value;
        }
    }
}
