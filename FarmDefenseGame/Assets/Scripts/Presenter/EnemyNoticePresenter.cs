using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using WolfVillageBattle.Interface;

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

        }

        //  // 気づいた時の挙動
        //     this.OnTriggerEnterAsObservable()
        //         .Where(_ => _.gameObject.tag == "Player" && current_status == EnemyStatus.IDLE && anim_label == 0)
        //         .Subscribe(_ => {
        //             Debug.Log("気づいちゃったんだおー");
        //             StartCoroutine ("ChangeNoticeFromIdle");
        //     }).AddTo(this);
        
        /// <summary>
        /// idleからnoticeにステータスに変更する
        /// TODO: 絶対にモデルを変更した時にこれはなくなる
        /// </summary>
        // private IEnumerator ChangeNoticeFromIdle()
        // {
        //     anim_label = 1;
        //     SetEnemyStatus(EnemyStatus.ANIMATION);
        //     AudioManager.Instance.PlaySE("slime_found");
        //     GetComponent<Animator>().Play("Victory");
        //     yield return new WaitForSeconds(4.0f);
        //     // 攻撃とか受けてステータスが変わってないことを見る
        //     if (current_enemy_status == EnemyStatus.ANIMATION) SetEnemyStatus(EnemyStatus.NOTICE);
        //     anim_label = 0;
        // }
    }
}

