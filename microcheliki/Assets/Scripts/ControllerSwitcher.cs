using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    [HideInInspector]
    public bool isFollowingCar = false;
    public bool isFollowingPlayer = true;
    
    [Header("Elements")]
    public PlayerController playerController;
    public CarController carController;
    public CameraHolder cameraHolder;

    private void Update()
    {
        HandleSwitch();
    }

    public void HandleSwitch()
    {
        if (isFollowingPlayer)
        {
            cameraHolder.FollowPlayer();
            playerController.enabled = true;
            carController.enabled = false;
        }
        else if (isFollowingCar)
        {
            cameraHolder.FollowCar();
            carController.enabled = true;
            playerController.enabled = false;
        } 
        if (Input.GetKeyDown(KeyCode.L))
        {
            isFollowingPlayer = false;
            isFollowingCar = true;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            isFollowingCar = false;
            isFollowingPlayer = true;
        }
    }
}
