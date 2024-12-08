using UnityEngine;
using R3;
using R3.Triggers;
using WolfVillage.Battle.Interface;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Battle
{
    public class EnemyNoticePresenter : MonoBehaviour
    {
        public void Initialize(IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            IEnemyNoticeUseCase enemyNoticeUseCase = new EnemyNoticeActor(enemyView, enemyEntity);
            enemyView.GameObject.OnTriggerEnterAsObservable()
                .Where(_ => _.gameObject.tag == "Player")
                .Subscribe(_ => {
                    enemyNoticeUseCase.EnemyNotice();
                }).AddTo(this);

            enemyView.GameObject.OnTriggerExitAsObservable()
                .Where(_ => _.gameObject.tag == "Player")
                .Subscribe(_ => {
                    enemyNoticeUseCase.EnemyOverlook();
                }).AddTo(this);
        }
    }
}

