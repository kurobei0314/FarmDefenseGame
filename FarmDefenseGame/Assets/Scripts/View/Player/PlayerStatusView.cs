using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle 
{
    // プレイヤーのアニメーションでStatusが変更されるなどのためだけに作られたクラス
    // なんか違う気がする
    public class PlayerStatusView : MonoBehaviour
    {
        private IPlayerEntity playerEntity;

        public void Initialize (MainGameRepository mainGameRepository)
        {
            playerEntity = mainGameRepository.Player;
        }
        
        public void SetIdleStatus()
        {
            playerEntity.SetStatus(Status.Idle);
        }

        public void ResetTimeScaler()
        {
            var timeScaler = (ITimeScaler) new TimeScaler();
            timeScaler.SetTimeScaler(1.0f);
        }
    }
}