using UnityEngine;
using WolfVillage.Battle.Interface;

namespace WolfVillage.Battle
{
    public class PlayerSEView : MonoBehaviour, ISound
    {
        public void PlayAttackSound()
        {
            float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
            if (rnd < 0.5f)
            {
                AudioManager.Instance.PlaySE("unitychan_attack1");
            } else {
                AudioManager.Instance.PlaySE("unitychan_attack2");
            }
        }

        public void PlayDamageSound()
        {
            float rnd = UnityEngine.Random.Range(0.0f, 1.0f);
            if (rnd < 0.5f){
                AudioManager.Instance.PlaySE("unitychan_damage1");
            } else{
                AudioManager.Instance.PlaySE("unitychan_damage2");
            }
        }

        public void PlayDieSound()
        {
            AudioManager.Instance.PlaySE("unitychan_lose");
        }
    }
}
