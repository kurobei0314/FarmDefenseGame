using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 絶対に書き方を変えるんだ、、、
public class RotateHPBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
