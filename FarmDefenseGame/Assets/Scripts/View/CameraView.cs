using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour, ICameraView
{
    public GameObject GameObject => this.gameObject;
}

public interface ICameraView
{
    GameObject GameObject { get; }
}
