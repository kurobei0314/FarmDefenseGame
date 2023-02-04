using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.UpArrow))
            .Subscribe(_ => {
                Vector3 pos = this.transform.position;
                this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(0.0f, 0.0f, 0.05f));
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.DownArrow))
            .Subscribe(_ => {
                Vector3 pos = this.transform.position;
                this.GetComponent<Rigidbody>().MovePosition(pos + this.transform.rotation * new Vector3(0.0f, 0.0f, -0.05f));
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.RightArrow))
            .Subscribe(_ => {
                this.transform.Rotate(0.0f, 0.05f, 0.0f);
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.LeftArrow))
            .Subscribe(_ => {
                this.transform.Rotate(0.0f, -0.05f, 0.0f);
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyUp(KeyCode.UpArrow)    ||  Input.GetKeyUp(KeyCode.DownArrow) || 
                        Input.GetKeyUp(KeyCode.RightArrow) ||  Input.GetKeyUp(KeyCode.LeftArrow))
            .Subscribe(_ => {
                GetComponent<Animator>().Play("Standing(loop)");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
