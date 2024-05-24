// Following Next Tutorial: https://www.youtube.com/watch?v=2IhzPTS4av4
// a.a.
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupAndDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    private ObjectGrabbable objectGrabbable;
    [SerializeField] private float pickupDistance = 2f;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                if( Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
            }



        }
    }


}
