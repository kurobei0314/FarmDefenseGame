using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class TimeScaler : ITimeScaler
    {
        public void SetTimeScaler(float timeScale)
        {
            Time.timeScale = timeScale;
        }
    }
}
