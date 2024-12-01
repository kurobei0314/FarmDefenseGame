using System;
using System.Collections;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace WolfVillageBattle
{
    public class PlayerSkillIconView : MonoBehaviour
    {
        [SerializeField] private Image IconImage = null;
        [SerializeField] private Image NotSetImage = null;
        [SerializeField] private Image CannotUseSkillImage = null;

        public Observable<int> AbleUseSkillObservable => ableUseSkill;
        private Subject<int> ableUseSkill = new Subject<int>();

        private int index;
        private float intervalTime;
        private string iconImageName;

        public void Initialize(int index, float intervalTime, string iconImageName)
        {
            this.index = index;
            this.intervalTime = intervalTime;
            this.iconImageName = iconImageName;
            IconImage.gameObject.SetActive(true);
            NotSetImage.gameObject.SetActive(false);
        }

        public void InitializeForNotSetSkill()
        {
            IconImage.gameObject.SetActive(false);
            NotSetImage.gameObject.SetActive(true);
        }

        public void UpdateViewForUseSkill()
        {
            CannotUseSkillImage.gameObject.SetActive(true);
            StartCoroutine(UpdateViewAfterIntervalTime());
        }

        private IEnumerator UpdateViewAfterIntervalTime()
        {
            yield return new WaitForSeconds(intervalTime);
            CannotUseSkillImage.gameObject.SetActive(false);
            ableUseSkill.OnNext(index);
        }
    }
}
