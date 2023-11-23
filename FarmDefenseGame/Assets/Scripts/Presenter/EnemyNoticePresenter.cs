using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using WolfVillageBattle.Interface;
using TMPro;

namespace WolfVillageBattle
{
    public class EnemyNoticePresenter : MonoBehaviour
    {
        public void Initialize(IEnemyView enemyView, IEnemyEntity enemyEntity)
        {
            IEnemyNoticeUseCase enemyNoticeUseCase = new EnemyNotiveActor(enemyView, enemyEntity);
            this.OnTriggerEnterAsObservable()
                .Where(_ => _.gameObject.tag == "Player")
                .Subscribe(_ => {
                    enemyNoticeUseCase.EnemyNotice();
                }).AddTo(this);

            this.OnTriggerExitAsObservable()
                .Where(_ => _.gameObject.tag == "Player")
                .Subscribe(_ => {
                    enemyNoticeUseCase.EnemyOverlook();
                });
        }
    }
}

