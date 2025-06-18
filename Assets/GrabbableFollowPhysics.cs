using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableFollowPhysics : MonoBehaviour
{
    public Transform followTarget;

    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Prevents interpolation issues
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    [System.Obsolete]
    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    [System.Obsolete]
    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Automatically assign the hand/controller transform
        var interactorTransform = args.interactorObject.transform;

        // Optional: Check left vs. right
        string interactorName = interactorTransform.name.ToLower();
        if (interactorName.Contains("left"))
            Debug.Log("Grabbed with LEFT hand");
        else if (interactorName.Contains("right"))
            Debug.Log("Grabbed with RIGHT hand");

        followTarget = interactorTransform;
    }

    [System.Obsolete]
    private void OnRelease(SelectExitEventArgs args)
    {
        followTarget = null;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (followTarget == null) return;

        // Linear movement
        Vector3 velocityTarget = (followTarget.position - transform.position) / Time.fixedDeltaTime;
        rb.velocity = velocityTarget;

        // Angular movement
        Quaternion rotationDiff = followTarget.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angleInDeg, out Vector3 rotationAxis);
        Vector3 angularTarget = angleInDeg * rotationAxis * Mathf.Deg2Rad / Time.fixedDeltaTime;
        rb.angularVelocity = angularTarget;
    }
}
