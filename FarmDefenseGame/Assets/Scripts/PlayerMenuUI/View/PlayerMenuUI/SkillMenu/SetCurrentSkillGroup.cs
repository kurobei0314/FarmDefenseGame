using UnityEngine;
using WolfVillage.Entity.Interface;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SetCurrentSkillGroup : MonoBehaviour
    {
        [SerializeField] private SetSkillRoleTypeIcon _currentRoleTypeIcon;
        [SerializeField] private SetCurrentSkill[] _currentSkills;

        public void Initialize(RoleType type, ISkillEntity[] currentSkills)
        {
            _currentRoleTypeIcon.Initialize(type);
            for (var i = 0; i < GameInfo.PLAYER_SET_SKILL_NUM; i++)
            {
                _currentSkills[i].Initialize(currentSkills[i]);
            }
            // TODO: アイコンの表示をする
        }

        public void UpdateFocusView(int focusIndex, bool isFocus)
        {
            if (focusIndex < 0 || GameInfo.PLAYER_SET_SKILL_NUM <= focusIndex) return;
            _currentSkills[focusIndex].SetFocus(isFocus);
        }

        public void Dispose()
        {
            _currentRoleTypeIcon.Dispose();
            for (var i = 0; i < GameInfo.PLAYER_SET_SKILL_NUM; i++)
            {
                _currentSkills[i].Dispose();
            }
        }
    }
}
