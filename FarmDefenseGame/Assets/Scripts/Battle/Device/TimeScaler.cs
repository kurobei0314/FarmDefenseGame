using UnityEngine;
using WolfVillage.Battle.Interface;

namespace WolfVillage.Battle
{
    public class TimeScaler : ITimeScaler
    {
        public void SetTimeScaler(float timeScale)
        {
            Time.timeScale = timeScale;
        }
    }
}
