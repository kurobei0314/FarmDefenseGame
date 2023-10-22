using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialize()
    {

        // // カメラの移動
        // Observable.EveryUpdate()
        //     .Where(_ => Input.GetAxis("CameraMove") != 0)
        //     .Subscribe(_ => {
        //         float cameraInput = Input.GetAxis("CameraMove");
        //         camera.transform.RotateAround(unity_chan.gameObject.transform.position, Vector3.up, cameraInput * 0.7f);
        // }).AddTo(this);
    }
}
