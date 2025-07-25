using TMPro;
using UnityEngine;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public class SkillDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text _description;

        public void SetText(string text)
            => _description.text = text;

        public void Open()
            => this.gameObject.SetActive(true);

        public void Close()
            => this.gameObject.SetActive(false);
    }
}
