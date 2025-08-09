using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WolfVillage.Search.PlayerMenuUI.EquipmentMenu
{
    public class EquipmentPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;

        // TODO: 将来的には、アイコンの情報を持ったinterfaceを渡してそこからiconを表示するようにする
        public void Initialize(string name)
        {
            _name.text = name;
        }

        public void Dispose()
        {
            _icon.sprite = null;
        }
    }
}
