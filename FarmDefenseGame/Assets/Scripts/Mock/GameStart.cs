using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("title");
        // TODO: 今は、GetButtonDown("attack")にしてるけど、本当は良くないからね。直してね。
        Observable.EveryUpdate()
            .Where(_ => Input.GetButtonDown("Attack"))
            .Subscribe(_ => {
                AudioManager.Instance.PlaySE("system");
                Initiate.Fade("Main", Color.black, 1.0f);
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}