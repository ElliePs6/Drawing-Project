using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

[RequireComponent(typeof(XRRayInteractor))]
public class RayGrabListener : MonoBehaviour
{
    [SerializeField] private float grabSnapRange = 0.5f;

    private XRRayInteractor interactor;

    void Awake()
    {
        interactor = GetComponent<XRRayInteractor>();
        interactor.selectEntered.AddListener(OnGrabbed);
        interactor.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        GameObject grabbedObject = ((Component)args.interactableObject).gameObject;
        float distance = Vector3.Distance(transform.position, grabbedObject.transform.position);

        Debug.Log($"{gameObject.name} grabbed {grabbedObject.name} via Ray (Distance: {distance})");

        if (distance <= grabSnapRange)
        {
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.rotation = transform.rotation;
            Debug.Log($"Snapped {grabbedObject.name} to hand.");
        }
        else
        {
            Debug.Log($"{grabbedObject.name} was too far to snap.");
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        GameObject releasedObject = ((Component)args.interactableObject).gameObject;
        Debug.Log($"{gameObject.name} released {releasedObject.name}");
    }

    void OnDestroy()
    {
        if (interactor != null)
        {
            interactor.selectEntered.RemoveListener(OnGrabbed);
            interactor.selectExited.RemoveListener(OnReleased);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, grabSnapRange);
    }
}
