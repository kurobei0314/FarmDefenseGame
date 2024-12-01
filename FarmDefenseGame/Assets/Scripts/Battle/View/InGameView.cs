using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle
{
    public class InGameView : MonoBehaviour, IInGameView
    {
        [SerializeField] private HPBarView _playerHPBarView = null;
        [SerializeField] private Transform _attackTextParent = null;
        [SerializeField] private PlayerSkillIconView[] _playerSkillIconViews = null;

        public PlayerSkillIconView[] PlayerSkillIconViews => _playerSkillIconViews;

        public void Initialize(float playerMaxHP, ISkillEntity[] playerSkillEntities)
        {
            UpdatePlayerHPView(playerMaxHP);
            UpdateJustAvoidViewActive(false);

            for (var i = 0; i < _playerSkillIconViews.Length; i++) 
            {
                if (playerSkillEntities[i] == null) 
                {
                    _playerSkillIconViews[i].InitializeForNotSetSkill();
                    continue;
                }

                var skillVO = playerSkillEntities[i].SkillVO;
                _playerSkillIconViews[i].Initialize(i, skillVO.IntervalTime, skillVO.IconImageName);
            }
        }

        public void UpdatePlayerHPView(float currentHPRatio)
            => _playerHPBarView.SetValue(currentHPRatio);

        public void UpdateJustAvoidViewActive(bool active)
            => _attackTextParent.gameObject.SetActive(active);

        public void UpdateSkillIconViewForUseSkill(int index)
            => _playerSkillIconViews[index].UpdateViewForUseSkill();
    }
}
