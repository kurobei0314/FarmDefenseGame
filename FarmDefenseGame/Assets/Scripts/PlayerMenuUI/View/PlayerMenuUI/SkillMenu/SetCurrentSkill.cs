using UnityEngine;
using UnityEngine.UI;
using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SetCurrentSkill : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _iconImage;

        public void Initialize(ISkillEntity skillEntity)
        {
            _iconImage.gameObject.SetActive(skillEntity != null);
        }

        public void SetFocus(bool isFocus)
            => _animator.SetBool("Select", isFocus);

        public void Dispose()
        {
            _iconImage = null;
        }
    }
}
