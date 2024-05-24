using System.Collections;
using System.Collections;
using UnityEngine;

public class AttachingObject : MonoBehaviour
{
    [Header("Car Properties")]
    [SerializeField] private string itemTag; 

    [Header("Wheel Properties")]
    [SerializeField] private Transform attachPoint; 

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
        if (other.CompareTag(itemTag) && !isAttached)
        {
            Debug.Log("Collided with car: " + itemTag);

            
            GameObject item = Instantiate(other.gameObject, attachPoint.position, Quaternion.identity);


            item.layer = LayerMask.NameToLayer("Default");

            Rigidbody wheelRigidbody = item.GetComponent<Rigidbody>();
            if (wheelRigidbody == null)
            {
                Debug.LogError("Rigidbody not found on the collided object.");
                return;
            }

            AttachWheelToCar(wheelRigidbody);

            
            Destroy(other.gameObject);
        }
    }

    public void AttachWheelToCar(Rigidbody wheelRigidbody)
    {
        
        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = wheelRigidbody;

        
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




