using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class EnemySEView : MonoBehaviour, IEnemySound, ISound
    {
        public void PlayAttackSound()
        {
            throw new System.NotImplementedException();
        }

        public void PlayNoticeSound()
        {
            AudioManager.Instance.PlaySE("slime_found");
        }

        public void PlayOverlookSound()
        {
            throw new System.NotImplementedException();
        }
    }
}
