
using UnityEngine;

namespace WolfVillageBattle
{
    public class EnemyHPBarView : HPBarView
    {
        void LateUpdate()
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
