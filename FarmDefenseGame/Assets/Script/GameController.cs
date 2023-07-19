using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

// MEMO: <ゲームダンジョン用> 
public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerTest player;

    [SerializeField]
    private GameObject game_over;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("main");
        game_over.SetActive(false);

        player.PlayerDieObservable.Subscribe(index => {
            game_over.SetActive(true);
        }).AddTo(this);
        
        // タイトル画面に戻る (キーボードのQまたは、PSボタン)
        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.JoystickButton9))
            .Subscribe(_ => {
                Initiate.Fade("GameStart", Color.black, 1.0f);
        }).AddTo(this);

        // タイトル画面に戻る(xボタン)
        Observable.EveryUpdate()
            .Where(_ => player.CurrentStatus == PlayerTest.PlayerStatus.DIE && Input.GetKey(KeyCode.JoystickButton1)
                        )
            .Subscribe(_ => {
                Initiate.Fade("GameStart", Color.black, 1.0f);
        }).AddTo(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
