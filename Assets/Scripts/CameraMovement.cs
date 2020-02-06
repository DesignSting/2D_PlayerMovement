using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CameraLocked cameraLockType;

}

public enum CameraLocked
{
    HorizonalLocked,
    VerticalLocked,
    BothLocked
}
