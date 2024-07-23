using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    [Header("Elements")]
    public PlayerController playerController;
    public CarController carController;
    public CameraHolder cameraHolder;

    private void Awake()
    {
        carController.DeactivateScript();
        playerController.ActivateScript();
        cameraHolder.FollowPlayer();
    }

    private void Update()
    {
        HandleSwitch();
    }

    public void HandleSwitch()
    {
        if (carController.scriptIsActive)
        {
            cameraHolder.FollowCar();
        }
        if (playerController.scriptIsActive)
        {
            cameraHolder.FollowPlayer();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            carController.ActivateScript();
            playerController.DeactivateScript();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            carController.DeactivateScript();
            playerController.ActivateScript();
        }
    }
}
