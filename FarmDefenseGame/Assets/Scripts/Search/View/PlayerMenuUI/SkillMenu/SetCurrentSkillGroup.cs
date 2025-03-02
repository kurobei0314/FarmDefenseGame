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
            for (var i = 0; i < _currentSkills.Length; i++)
            {
                _currentSkills[i].Initialize(currentSkills[i]);
            }
            // TODO: アイコンの表示をする
        }

        public void Dispose()
        {
            _currentRoleTypeIcon.Dispose();
            for (var i = 0; i < _currentSkills.Length; i++)
            {
                _currentSkills[i].Dispose();
            }
        }
    }
}
