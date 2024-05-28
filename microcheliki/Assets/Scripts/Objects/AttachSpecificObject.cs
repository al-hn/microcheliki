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

  

    [HideInInspector]
    public bool isAttached = false;
    private Rigidbody carRigidbody;
    private FixedJoint fixedJoint;

    private void Start()
    {
        carRigidbody = GameObject.FindGameObjectWithTag(itemTag)?.GetComponent<Rigidbody>();
        if (carRigidbody == null)
        {
            Debug.LogError("Car Rigidbody not found or car tag is incorrect.");
        }
    }

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

            AttachObjectToCar(objectRigidbody);

            // Set the turret in the Upgrade UI
            Turret turret = item.GetComponent<Turret>();
          

            Destroy(other.gameObject);
        }
    }

    public void AttachObjectToCar(Rigidbody objectRigidbody)
    {
        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = objectRigidbody;

        Vector3 targetPosition = attachPoint.position;

        StartCoroutine(SmoothMoveToAttachPoint(targetPosition));

        isAttached = true;
    }

    private IEnumerator SmoothMoveToAttachPoint(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            yield return null;
        }

        transform.position = targetPosition;
    }
}

