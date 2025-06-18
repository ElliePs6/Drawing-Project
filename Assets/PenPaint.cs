using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PenPaint : MonoBehaviour{

    
 

     public Texture2D paintTexture;
    public Color brushColor = Color.black;
    public int brushSize = 4;
 
    private Collider canvasCollider;
    private Renderer canvasRenderer;
    private Color[] brushColors;

    
   
    /// <Controllers>
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable  grabbable;
    public InputActionReference leftTriggerAction;
    public InputActionReference rightTriggerAction;
    private XRBaseInteractor interactorHoldingPen;

    /// <Controllers>

    private void OnEnable()
    {
        grabbable.selectEntered.AddListener(OnGrab);
        grabbable.selectExited.AddListener(OnRelease);     
    }

    private void OnDisable()
    {
        grabbable.selectEntered.RemoveListener(OnGrab);
        grabbable.selectExited.RemoveListener(OnRelease);

    }  

    
    private void OnGrab(SelectEnterEventArgs args)

    {
        interactorHoldingPen = args.interactorObject.transform.GetComponent<XRBaseInteractor>();

    }
    private void OnRelease(SelectExitEventArgs args)
    {
        interactorHoldingPen = null;

    }


  private void Start()
    {
        // Pre-compute brush color pixels
        brushColors = new Color[brushSize * brushSize];
        for (int i = 0; i < brushColors.Length; i++)
            brushColors[i] = brushColor;
 
        // Setup collider if needed
        if (!TryGetComponent(out SphereCollider col))
        {
            col = gameObject.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.002f; // 2mm precision
        }
 
        if (!TryGetComponent(out Rigidbody rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }
 
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("PaintCanvas")) return;
 
        canvasCollider = other;
        canvasRenderer = other.GetComponent<Renderer>();
 
       if (TryGetUVFromRay(transform.position, -transform.forward, out Vector2 uv))
        {
            DrawAtUV(uv);
        }
    }
 private bool TryGetUVFromRay(Vector3 origin, Vector3 direction, out Vector2 uv)
{
    uv = Vector2.zero;
    Ray ray = new Ray(origin, direction);

    if (canvasCollider.Raycast(ray, out RaycastHit hit, 0.05f))
    {
        uv = hit.textureCoord;
        return true;
    }

    return false;
}

 
    private void DrawAtUV(Vector2 uv)
    {
        int x = (int)(uv.x * paintTexture.width);
        int y = (int)(uv.y * paintTexture.height);
 
        paintTexture.SetPixels(x - brushSize / 2, y - brushSize / 2, brushSize, brushSize, brushColors);
        paintTexture.Apply();
    }
    
 


  


}







