using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfVillageBattle.Interface
{
    public enum CameraMode
    {
        Free,
        TargetLock,
    }

    public interface ICameraEntity 
    {
        CameraMode CurrentCameraMode { get; }
        public void SwitchCameraMode();
    }
}
