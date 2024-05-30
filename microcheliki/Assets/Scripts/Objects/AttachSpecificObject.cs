using System.Collections;
using UnityEngine;

public class AttachSpecificObject : MonoBehaviour
{
    [Header("Car Properties")]
    [SerializeField] private string itemTag;

    [Header("Object Properties")]
    [SerializeField] private Transform attachPoint;

    [Header("Specific Object")]
    [SerializeField] private GameObject objectToInstantiate;

    [Header("Attachment Properties")]
    [SerializeField] private float lerpSpeed = 10.0f;

    [Header("Ignored Rigidbody")]
    [SerializeField] private Rigidbody ignoredRigidbody;

    [HideInInspector]
    public bool isAttached = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with " + other.gameObject.name);

        if (other.CompareTag(itemTag) && !isAttached)
        {
            Debug.Log("Collided with car: " + itemTag);

            if (objectToInstantiate == null)
            {
                Debug.LogError("Object to instantiate is not assigned.");
                return;
            }

            GameObject item = Instantiate(objectToInstantiate, attachPoint.position, Quaternion.identity);
            Debug.Log("Instantiated object: " + item.name);

            item.layer = LayerMask.NameToLayer("Default");

            Rigidbody objectRigidbody = item.GetComponent<Rigidbody>();
            if (objectRigidbody == null)
            {
                Debug.LogError("Rigidbody not found on the instantiated object.");
                Destroy(item);
                return;
            }

            // Check if the object to instantiate has the ignored Rigidbody
            if (objectRigidbody == ignoredRigidbody)
            {
                Debug.Log("Instantiated object's Rigidbody is set to be ignored.");
                return;
            }

            AttachObjectToCar(objectRigidbody);

            // Set the turret in the Upgrade UI if needed
            Turret turret = item.GetComponent<Turret>();
            // Add any additional code for handling the turret here

            Destroy(other.gameObject);
        }
    }

    public void AttachObjectToCar(Rigidbody objectRigidbody)
    {
        Vector3 targetPosition = attachPoint.position;

        StartCoroutine(SmoothMoveToAttachPoint(targetPosition, objectRigidbody));

        isAttached = true;
    }

    private IEnumerator SmoothMoveToAttachPoint(Vector3 targetPosition, Rigidbody objectRigidbody)
    {
        while (Vector3.Distance(objectRigidbody.transform.position, targetPosition) > 0.01f)
        {
            objectRigidbody.transform.position = Vector3.Lerp(objectRigidbody.transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            yield return null;
        }

        objectRigidbody.transform.position = targetPosition;
        objectRigidbody.transform.parent = attachPoint;
        objectRigidbody.isKinematic = true; // Make the Rigidbody kinematic after attachment
    }
}


