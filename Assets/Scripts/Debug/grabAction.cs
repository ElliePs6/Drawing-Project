using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabAction : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    void Awake()
    {
        grabInteractable = GetComponent< UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable > ();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {

        Debug.Log(args.interactorObject);
        isGrabbed = true;

        if (isGrabbed)
        {
           //Debug.Log("Object is being grabbed!");
            // Do something while it's grabbed
        }
        else
        {
            //Debug.Log("Some fallback action");
            // Do something else (this will never happen here, but you can move logic elsewhere if needed)
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
       // Debug.Log("Object Released!");
    }

}
