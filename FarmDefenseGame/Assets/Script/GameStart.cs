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
        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.A))
            .Subscribe(_ => {
                Initiate.Fade("Main", Color.black, 1.0f);
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
