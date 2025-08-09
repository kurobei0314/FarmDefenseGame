using UnityEngine;
using UnityEngine.UI;
using WolfVillage.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SkillRoleTypeToggle : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private RoleType _type;

        public void Initialize(RoleType roleType)
        {
            _type = roleType;
        }
        
        public void SetOn()
        {
            _animator.SetBool("Select", true);
        }

        public void SetOff()
        {
            _animator.SetBool("Select", false);
        }

        public RoleType RoleType => _type;
    }
}
