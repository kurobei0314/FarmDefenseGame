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
    private GameObject enemies;

    [SerializeField]
    private GameObject game_clear;

    [SerializeField]
    private GameObject game_over;

    // MEMO: 完全なるゲームダンジョン用。全滅したかどうかをみる
    private int enemy_count;
    private int current_enemy_count;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("main");
        game_clear.SetActive(false);
        game_over.SetActive(false);
        enemy_count = enemies.transform.childCount;
        current_enemy_count = enemy_count;

        player.PlayerDieObservable.Subscribe(index => {
            game_over.SetActive(true);
        }).AddTo(this);

        for (int i = 0; i < enemy_count; i++) {
            var enemy = enemies.transform.GetChild(i).gameObject;
            enemy.GetComponent<EnemyTest>().EnemyDieObservable.Subscribe(_ => {
                current_enemy_count = current_enemy_count - 1;
                if(current_enemy_count <= 0){
                    player.GameClearPlayer();
                    game_clear.SetActive(true);
                }
            });
        }
        
        // タイトル画面に戻る (キーボードのQまたは、PSボタン)
        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.JoystickButton9))
            .Subscribe(_ => {
                Initiate.Fade("GameStart", Color.black, 1.0f);
        }).AddTo(this);

        // タイトル画面に戻る(xボタン)
        Observable.EveryUpdate()
            .Where(_ => (player.CurrentStatus == PlayerTest.PlayerStatus.DIE || player.CurrentStatus == PlayerTest.PlayerStatus.WIN) && 
                        Input.GetKey(KeyCode.JoystickButton1))
            .Subscribe(_ => {
                Initiate.Fade("GameStart", Color.black, 1.0f);
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
