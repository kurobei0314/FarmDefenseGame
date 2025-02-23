using System;
using UnityEngine;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SkillRoleTypeToggleGroup : MonoBehaviour
    {
        [SerializeField] public SkillRoleTypeToggle[] _roleTypeToggles;

        public void Initialize(RoleType setWeaponType)
        {
            var index = 0;
            foreach (var type in Enum.GetValues(typeof(RoleType)))
            {
                if (index >= _roleTypeToggles.Length) return;
                var roleType = (RoleType)type;
                _roleTypeToggles[index].Initialize(roleType);

                if (setWeaponType == roleType)
                {
                    _roleTypeToggles[index].SetOn();
                }
                index++;
            }
        }

        public void SwitchTab(RoleType type, Action setOnAction)
        {
            for (var i = 0; i < _roleTypeToggles.Length; i++)
            {
                if (_roleTypeToggles[i].RoleType == type)
                {
                    _roleTypeToggles[i].SetOn();
                    setOnAction();
                    return;
                }
                _roleTypeToggles[i].SetOff();
            }
        }
    }
}
