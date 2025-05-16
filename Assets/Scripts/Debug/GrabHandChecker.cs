using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HandGrabChecker : MonoBehaviour
{
    private XRDirectInteractor interactor;

    void Awake()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    void Update()
    { 

    }
    public void Grabbed(GameObject itemHolder) {
        Debug.Log("Object grabbed" + itemHolder.name);
    }

}
