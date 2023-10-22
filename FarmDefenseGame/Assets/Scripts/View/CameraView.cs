using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraView
{
    GameObject GameObject { get; }
    public void CameraMove(float cameraInput, Vector3 playerPosition);
}

public class CameraView : MonoBehaviour, ICameraView
{
    public GameObject GameObject => this.gameObject;

    public void CameraMove(float cameraInput, Vector3 playerPosition)
    {
        GameObject.transform.RotateAround(playerPosition, Vector3.up, cameraInput * 0.7f);
    }
}

