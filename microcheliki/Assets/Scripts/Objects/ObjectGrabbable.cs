// a.a.
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    [SerializeField] private float lerpSpeed = 10.0f;
    AttachingObject attachingObject;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();

    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidBody.useGravity = false;
    }
    

    public void Drop()
    {
        // if(!attachingObject.isAttached)
        // {
            this.objectGrabPointTransform = null;
            objectRigidBody.useGravity = true;
        // }
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPosition);
        }
    }
}
