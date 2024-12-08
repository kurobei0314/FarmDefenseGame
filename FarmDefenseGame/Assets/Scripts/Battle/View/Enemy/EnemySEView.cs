using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillage.Battle.Interface;

namespace WolfVillage.Battle
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

        public void PlayDamageSound()
        {
            // MEMO: 音を出さないっぽいが出すかもしれないので念の為メソッド作るだけ作っておく
            // AudioManager.Instance.PlaySE("slime_found");
            Debug.Log("ダメージのSEが指定されてないよ!");
        }

        public void PlayDieSound()
        {
            AudioManager.Instance.PlaySE("slime_die");
        }
    }
}
