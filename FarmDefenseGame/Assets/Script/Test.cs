using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.UpArrow))
            .Subscribe(_ => {
                Vector3 pos = this.transform.position;
                this.transform.position = new Vector3(pos.x, pos.y, pos.z + 0.05f);
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.DownArrow))
            .Subscribe(_ => {
                Vector3 pos = this.transform.position;
                this.transform.position = new Vector3(pos.x, pos.y, pos.z - 0.05f);
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.RightArrow))
            .Subscribe(_ => {
                //Vector3 rot = this.transform.rotation;
                this.transform.Rotate(0.0f, 0.05f, 0.0f);
                GetComponent<Animator>().Play("Running(loop)");
        });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.LeftArrow))
            .Subscribe(_ => {
                // Vector3 rot = this.transform.rotation;
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
