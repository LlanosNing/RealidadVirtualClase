using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class HeavyItemController : MonoBehaviour
{
    public float oneHandedDrag = 1.5f;           
    public float twoHandedDrag = 1f;             
    public float weightEffect = 3f;              
    public Transform headTransform;              

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private int handCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        
        rb.linearDamping = oneHandedDrag;
        rb.angularDamping = 0.5f;

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        handCount++;

        if (handCount == 1)
        {
            ApplyOneHandedProperties();
        }
        else if (handCount == 2)
        {
            ApplyTwoHandedProperties();
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        handCount--;

        if (handCount == 1)
        {
            ApplyOneHandedProperties();
        }
        else if (handCount == 0)
        {
            ResetProperties();
        }
    }

    private void ApplyOneHandedProperties()
    {
        rb.linearDamping = oneHandedDrag;
        grabInteractable.trackRotation = false;
        rb.isKinematic = false;
        ApplyWeightTorque();
    }

    private void ApplyTwoHandedProperties()
    {
        rb.linearDamping = twoHandedDrag;
        grabInteractable.trackRotation = true;
        rb.isKinematic = false;
        rb.angularVelocity = Vector3.zero;
    }

    private void ResetProperties()
    {
        rb.linearDamping = twoHandedDrag;
        grabInteractable.trackRotation = true;
        rb.isKinematic = false;
        rb.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        rb.angularDamping = 0.5f; 

        if (handCount == 1)
        {
            ApplyWeightTorque();
        }
    }

    private void ApplyWeightTorque()
    {
        Vector3 directionToHead = headTransform.position - transform.position;
        Vector3 weightTorque = Vector3.Cross(directionToHead.normalized, Vector3.down) * weightEffect;
        rb.AddTorque(weightTorque, ForceMode.Acceleration);
    }

    //Querido alumno, si alguna vez abres esto para entender c�mo funciona, haberme pedido que lo explicara en clase, un saludo. -�lvaro
}
