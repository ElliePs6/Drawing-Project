using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Pen : MonoBehaviour
{
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth = 0.01f;
    public Color[] penColors;

    [Header("XR Interaction")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable  grabbable;
    public InputActionReference rightTriggerAction;
    public InputActionReference leftTriggerAction;
    public InputActionReference changeColorAction;

    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactorHoldingPen;

    private void OnEnable()
    {
        grabbable.selectEntered.AddListener(OnGrab);
        grabbable.selectExited.AddListener(OnRelease);

        changeColorAction.action.performed += ctx => SwitchColor();
    }

    private void OnDisable()
    {
        grabbable.selectEntered.RemoveListener(OnGrab);
        grabbable.selectExited.RemoveListener(OnRelease);

        changeColorAction.action.performed -= ctx => SwitchColor();
    }

    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
    }

    private void Update()
    {
        //Debug.Log(interactorHoldingPen == null);
        if (interactorHoldingPen != null)
        {
            bool triggerPressed = false;
            Debug.Log(interactorHoldingPen.transform.parent.name);
            if (interactorHoldingPen.transform.parent.name.Contains("Right"))
            {
                triggerPressed = rightTriggerAction.action.IsPressed();
                Debug.Log("Right trigger pressed: " + triggerPressed);
            }
            else if (interactorHoldingPen.transform.parent.name.Contains("Left"))
            {
                triggerPressed = leftTriggerAction.action.IsPressed();
                Debug.Log("Left trigger pressed: " + triggerPressed);
            }

            if (triggerPressed)
            {
                Debug.Log("Calling Draw()");
                Draw();
            }
            else if (currentDrawing != null)
            {
                Debug.Log("Stopping drawing");
                currentDrawing = null;
            }
        }
    }


    private void OnGrab(SelectEnterEventArgs args)
    {
        interactorHoldingPen = args.interactorObject.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor>();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        interactorHoldingPen = null;
        currentDrawing = null;
    }

    private void Draw()
    {
        if (currentDrawing == null)
        {
            index = 0;
            currentDrawing = new GameObject("DrawingLine").AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.position);
        }
        else
        {
            var currentPos = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPos, tip.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.position);
            }
        }
    }

    private void SwitchColor()
    {
        currentColorIndex = (currentColorIndex + 1) % penColors.Length;
        tipMaterial.color = penColors[currentColorIndex];
    }
}
