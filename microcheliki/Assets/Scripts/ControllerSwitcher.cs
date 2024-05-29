using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    private CameraHolder cameraHolder;
    private CarController carController;
    private PlayerController playerController;
    private bool isFollowingCar = false;
    private bool isFollowingPlayer = true;

    private void Start()
    {
        cameraHolder = GameObject.Find("CameraHolder").GetComponent<CameraHolder>();
        carController = GameObject.Find("Muscle").GetComponent<CarController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

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