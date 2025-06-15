using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorCycleController : MonoBehaviour
{
    [Header("Material Settings")]
    public Material targetMaterial;

    [Tooltip("List of colors to cycle through.")]
    public Color[] colorSequence;

    [Tooltip("Delay in seconds between color changes.")]
    public float changeInterval = 2f;

    private int currentColorIndex = 0;
    private float timer;

    private void Start()
    {
        ApplyColor();
    }

    private void Update()
    {
        if (targetMaterial == null || colorSequence == null || colorSequence.Length == 0)
            return;

        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            timer = 0f;
            currentColorIndex = (currentColorIndex + 1) % colorSequence.Length;
            ApplyColor();
            Debug.Log("alaksa");
        }
    }

    private void ApplyColor()
    {
        targetMaterial.SetColor("_BaseColor", colorSequence[currentColorIndex]);
    }
}