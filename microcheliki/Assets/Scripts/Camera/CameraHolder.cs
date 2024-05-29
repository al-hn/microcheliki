using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [Header("Player's Camera Position")]
    public Transform cameraPosition;
    [Header("Car's Camera Position")]
    public Transform carCameraPosition;

    public void FollowPlayer()
    {
        transform.position = cameraPosition.position;
    }

    public void FollowCar()
    {
        transform.position = carCameraPosition.position;
    }
}
