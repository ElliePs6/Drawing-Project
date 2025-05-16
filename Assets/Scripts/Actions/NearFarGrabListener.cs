using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

[RequireComponent(typeof(NearFarInteractor))]
public class NearFarGrabListener : MonoBehaviour
{
    [SerializeField]
    private float grabSnapRange = 0.5f; // Adjustable in the Inspector

    private NearFarInteractor interactor;

    void Awake()
    {
        interactor = GetComponent<NearFarInteractor>();
        interactor.selectEntered.AddListener(OnGrabbed);
        interactor.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (args.interactableObject is Component component)
        {
            GameObject grabbedObject = component.gameObject;
            float distance = Vector3.Distance(transform.position, grabbedObject.transform.position);

            Debug.Log($"{gameObject.name} grabbed {grabbedObject.name} (Distance: {distance})");

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
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (args.interactableObject is Component component)
        {
            GameObject releasedObject = component.gameObject;
            Debug.Log($"{gameObject.name} released {releasedObject.name}");
        }
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, grabSnapRange);
    }
}
