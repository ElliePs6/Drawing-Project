using UnityEngine;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Hands.Samples.GestureSample;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandFistGrab : MonoBehaviour
{
    /** public StaticHandGesture fistGesture; // The gesture script with "Fist"
   // public UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor handInteractor; // The hand's Near/Far interactor
   // public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable penInteractable; // The pen object

    private bool isGrabbing;

    private void OnEnable()
    {
        if (fistGesture != null)
        {
            Debug.Log("[HandFistGrab] Subscribed to gesture events.");
            fistGesture.gesturePerformed.AddListener(OnFist);
            fistGesture.gestureEnded.AddListener(OnRelease);
        }
        else
        {
            Debug.LogWarning("[HandFistGrab] No fistGesture assigned.");
        }
    }

   private void OnDisable()
    {
        if (fistGesture != null)
        {
            Debug.Log("[HandFistGrab] Unsubscribed from gesture events.");
            fistGesture.gesturePerformed.RemoveListener(OnFist);
            fistGesture.gestureEnded.RemoveListener(OnRelease);
        }
  

    public void OnFist()
    {
        Debug.Log("[HandFistGrab] Fist gesture detected.");

      /**     if (isGrabbing)
        {
            Debug.Log("[HandFistGrab] Already grabbing.");
            return;
        }  **/

      
      /**  if (handInteractor == null || penInteractable == null)
        {
            Debug.LogWarning("[HandFistGrab] Missing interactor or interactable.");
            return;
        }

        if (handInteractor.interactionManager != null &&
            handInteractor.CanSelect((UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable) penInteractable))
        {
            Debug.Log("[HandFistGrab] Pen is selectable. Attempting to grab.");
            handInteractor.interactionManager.SelectEnter((IXRSelectInteractor)handInteractor, penInteractable);
            isGrabbing = true;
            Debug.Log("[HandFistGrab] Grab successful.");
        }
        else
        {
            Debug.Log("[HandFistGrab] Pen is NOT selectable by interactor.");
        }
    }

    public void OnRelease()
    {
        Debug.Log("[HandFistGrab] Gesture release detected.");
        return;

        if (!isGrabbing)
        {
            Debug.Log("[HandFistGrab] Not currently grabbing, skipping release.");
            return;
        }

        if (handInteractor == null || penInteractable == null)
        {
            Debug.LogWarning("[HandFistGrab] Missing interactor or interactable during release.");
            return;
        }

        if (handInteractor.hasSelection && handInteractor.firstInteractableSelected == penInteractable)
        {
            Debug.Log("[HandFistGrab] Releasing pen.");
            handInteractor.interactionManager.SelectExit((IXRSelectInteractor)handInteractor, penInteractable);
        }
        else
        {
            Debug.LogWarning("[HandFistGrab] Pen was not actually selected by interactor.");
        }

        isGrabbing = false;
    }  **/

}
