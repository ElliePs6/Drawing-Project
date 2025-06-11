using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Linq;

public class Pen : MonoBehaviour
{

    [SerializeField] public Transform tip;
    [SerializeField] private int penSize = 5;
    private Renderer _renderer;
    private Color[] brushColors;
    private float _tipHeight = 0.04f;
    private RaycastHit _touch;
    private PaintCanvas _paintcanvas;

    private Vector2 _touchPos;
    private Vector2 _lastTouchPos;
    private Quaternion _lastTouchRot;
    private bool _touchedLastFrame; 
    

    //public Material drawingMaterial;
    public Material tipMaterial;
    public Color[] colorPalette;
    //private Vector3 _frozenPosition;
    // private Quaternion _frozenRotation;

    //[Range(0.001f, 0.01f)]
    // public float tipWidth = 0.001f;







    [Header("XR Interaction")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable  grabbable;
    public InputActionReference leftTriggerAction;
    public InputActionReference rightTriggerAction;

    private int currentColorIndex;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactorHoldingPen;

    private void OnEnable()
    {
        grabbable.selectEntered.AddListener(OnGrab);
        grabbable.selectExited.AddListener(OnRelease);

       // changeColorAction.action.performed += ctx => SwitchColor();
    }

    private void OnDisable()
    {
        grabbable.selectEntered.RemoveListener(OnGrab);
        grabbable.selectExited.RemoveListener(OnRelease);

        //changeColorAction.action.performed -= ctx => SwitchColor();
    }


    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = colorPalette[currentColorIndex];
        //tip.localScale = new Vector3(tipWidth, tipWidth, tipWidth);

        _renderer = tip.GetComponent<Renderer>();
        Debug.Log("To xrwma einai " + _renderer.material.color.ToString());
        brushColors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
       // _tipHeight = tip.localScale.y;

        AllocateBrushColors();
    }

    private void Update()
    {
        //Debug.Log(interactorHoldingPen == null);
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



    private void OnGrab(SelectEnterEventArgs args)
    {
        interactorHoldingPen = args.interactorObject.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor>();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        interactorHoldingPen = null;
   
    }

    private void Draw()
    {
           
        if (Physics.Raycast(tip.position, transform.up, out _touch, _tipHeight))
        {
            Debug.Log("petuxa to " + _touch.transform.gameObject.name);
            if (_touch.transform.CompareTag("PaintCanvas"))
            {
                Debug.Log("eimai o canvas");
                if (_paintcanvas == null)
                {

                    _paintcanvas = _touch.transform.GetComponent<PaintCanvas>();
                    Debug.Log("eimai o paintcanvas" + _paintcanvas);
                }
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _paintcanvas.textureSize.x - (penSize / 2));
                var y = (int)(_touchPos.y * _paintcanvas.textureSize.y - (penSize / 2));
                if (y < 0 || y > _paintcanvas.textureSize.y || x < 0 || x > _paintcanvas.textureSize.x) return;
                // = Mathf.Clamp(x, 0, (int)_paintcanvas.textureSize.x - penSize);
               // y = Mathf.Clamp(y, 0, (int)_paintcanvas.textureSize.y - penSize);

                if (_touchedLastFrame)
                {
                    _paintcanvas.texture.SetPixels(x, y, penSize, penSize, brushColors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)//how much percent do you want
                    {
                        Debug.Log("Mphka sth for");
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _paintcanvas.texture.SetPixels(lerpX, lerpY, penSize, penSize, brushColors);
                    }
                    transform.rotation = _lastTouchRot;
                    _paintcanvas.texture.Apply();
                    Debug.Log("I apply it");
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

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
         tipMaterial.color = newColor;

         // Update the brush color
         for (int i = 0; i < brushColors.Length; i++)
         {
             brushColors[i] = newColor;
         }
        _renderer.material.color = newColor;
     }

}
