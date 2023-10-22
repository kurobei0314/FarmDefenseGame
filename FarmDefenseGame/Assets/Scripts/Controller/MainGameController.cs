using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

namespace WolfVillageBattle {
    public class MainGameController : MonoBehaviour
    {
        [SerializeField]
        private MainGameRepository mainGameRepository;

        [SerializeField]
        private PlayerMoveController playerMoveController;

        [SerializeField]
        private PlayerAttackController playerAttackController;

        [SerializeField]
        private PlayerStatusView playerStatusView;

        // Start is called before the first frame update
        void Start()
        {
            mainGameRepository.Initialize();
            playerStatusView.Initialize(mainGameRepository);
            playerMoveController.Initialize(mainGameRepository);
            playerAttackController.Initialize(mainGameRepository);
        }
    }
}
