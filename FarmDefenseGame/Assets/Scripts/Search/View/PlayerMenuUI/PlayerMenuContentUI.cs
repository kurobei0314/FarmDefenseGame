using UnityEngine;
using UnityEngine.InputSystem;

namespace WolfVillage.Search.PlayerMenuUI
{
    public class PlayerMenuContentUI : MonoBehaviour
    {
        [SerializeField] private EquipmentMenuUI _weaponMenuUI;
        public void Initialize( PlayerInput playerInput,
                                ISetEquipmentUseCase equipmentUseCase)
        {
            _weaponMenuUI.Initialize(playerInput, equipmentUseCase);
        }
    }
}
