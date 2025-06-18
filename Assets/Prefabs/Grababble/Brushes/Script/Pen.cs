using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Pen : MonoBehaviour
{

    [SerializeField] public Transform tip;
    [SerializeField] private int penSize = 5;
    private Renderer _renderer;
    private Color[] brushColors;
    [SerializeField] private float _tipHeight = 0.01f;
    private RaycastHit _touch;
    private PaintCanvas _paintcanvas;

    private Vector2 _touchPos;
    private Vector2 _lastTouchPos;
    private Quaternion _lastTouchRot;
    private bool _touchedLastFrame; 
    public Material tipMaterial;
    public Color[] colorPalette;
    private int currentColorIndex;


    
   
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

  

    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = colorPalette[currentColorIndex];
        

        _renderer = tip.GetComponent<Renderer>();
      //  Debug.Log("To xrwma einai " + _renderer.material.color.ToString());
        brushColors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
       // _tipHeight = tip.localScale.y;

        AllocateBrushColors();
    }

    private void FixedUpdate()
    {
        if (interactorHoldingPen != null)
        {
            bool triggerPressed = false;
            //Debug.Log(interactorHoldingPen.transform.parent.name);
            if (interactorHoldingPen.transform.parent.name.Contains("Left") || interactorHoldingPen.transform.parent.name.Contains("Right"))
            {
                triggerPressed = leftTriggerAction.action.IsPressed();
                triggerPressed = rightTriggerAction.action.IsPressed();
                Debug.Log("kalw thn draw");
                Draw();

            }
        }

    }
    private void Update()
    {
      /**  Debug.DrawRay(grabbable.attachTransform.position, grabbable.attachTransform.forward * 0.1f, Color.green);
        Debug.DrawRay(grabbable.attachTransform.position, grabbable.attachTransform.up * 0.1f, Color.red);
        Debug.DrawRay(grabbable.attachTransform.position, grabbable.attachTransform.right * 0.1f, Color.blue);**/

       

    }
    private void OnGrab(SelectEnterEventArgs args)

    {
        this.GetComponent<BoxCollider>().enabled = false;
        interactorHoldingPen = args.interactorObject.transform.GetComponent<XRBaseInteractor>();
   
      
        //isHeldByHand = true;
    

       
    }
    private void OnRelease(SelectExitEventArgs args)
    {
        this.GetComponent<BoxCollider>().enabled = true;
        interactorHoldingPen = null;
        //isHeldByHand = false;

    }



    private void Draw()
    {
        Debug.Log("Trying to draw...");

        //if (Physics.Raycast(tip.position, tip.forward, out _touch, _tipHeight))

       if (Physics.Raycast(tip.position, transform.up, out _touch, _tipHeight))
        {
           // Debug.Log("Raycast hit: " + _touch.transform.gameObject.name);

            if (_touch.transform.CompareTag("PaintCanvas"))
            {
               // Debug.Log("Hit object is PaintCanvas");

                if (_paintcanvas == null)
                {
                    _paintcanvas = _touch.transform.GetComponent<PaintCanvas>();
                   // Debug.Log("Got PaintCanvas component: " + _paintcanvas);
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);
                var x = (int)(_touchPos.x * _paintcanvas.textureSize.x - (penSize / 2));
                var y = (int)(_touchPos.y * _paintcanvas.textureSize.y - (penSize / 2));

                x = Mathf.Clamp(x, 0, (int)_paintcanvas.textureSize.x - penSize);
                y = Mathf.Clamp(y, 0, (int)_paintcanvas.textureSize.y - penSize);

                if (_touchedLastFrame)
                {
                   // Debug.Log("Drawing pixels and interpolating...");
                    _paintcanvas.texture.SetPixels(x, y, penSize, penSize, brushColors);

                    if (Vector2.Distance(_lastTouchPos, new Vector2(x, y)) > 1f)
                    {
                        for (float f = 0.01f; f < 1.00f; f += 0.01f)
                        {
                            var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                            var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                            _paintcanvas.texture.SetPixels(lerpX, lerpY, penSize, penSize, brushColors);
                        }
                    }

                  transform.rotation = _lastTouchRot;
                    _paintcanvas.texture.Apply();
                   // Debug.Log("Applied texture changes.");
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
            else
            {
                //Debug.Log("Raycast hit something, but it's not a PaintCanvas.");
            }
        }
        else
        {
            //Debug.Log("Raycast did not hit anything.");
        }

        if (_paintcanvas != null)
           // Debug.Log("Resetting _paintcanvas");

        _paintcanvas = null;
        _touchedLastFrame = false;
    }


    private void AllocateBrushColors()
    {
        brushColors = new Color[penSize * penSize];
        for (int i = 0; i < brushColors.Length; i++)
            brushColors[i] = colorPalette[currentColorIndex];
    }


    public void SwitchColor(Color newColor)
    {
        //Debug.Log($"[SwitchColor] Called with color: {newColor}");

        // Update tip material color
        if (tipMaterial != null)
        {
            tipMaterial.color = newColor;
            //Debug.Log($"[SwitchColor] tipMaterial color set to: {tipMaterial.color}");
        }
        else
        {
            // Debug.LogWarning("[SwitchColor] tipMaterial is null");
        }

        // Update the brush color array
        for (int i = 0; i < brushColors.Length; i++)
        {
            brushColors[i] = newColor;
        }
        //Debug.Log($"[SwitchColor] brushColors array updated. First value: {brushColors[0]}");

        // Update the pen's renderer material
        if (_renderer != null)
        {
            _renderer.material.color = newColor;
            // Debug.Log($"[SwitchColor] _renderer material color set to: {_renderer.material.color}");
        }
        else
        {
            // Debug.LogWarning("[SwitchColor] _renderer is null");
        }
    }




}







