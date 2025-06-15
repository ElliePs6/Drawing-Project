using UnityEngine;

public class PalletteColors : MonoBehaviour
{
    private Color color;
    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            color = renderer.material.color;
        }
        else
        {
           // Debug.LogError("No Renderer found on " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
       // Debug.Log($"[OnTriggerEnter] Triggered by: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("BrushTip"))
        {
           // Debug.Log("[OnTriggerEnter] BrushTip tag matched.");

            Pen pen = collision.gameObject.GetComponent<Pen>();
            if (pen != null)
            {
               // Debug.Log("[OnTriggerEnter] Pen component found. Switching color...");
                pen.SwitchColor(color);
            }
            else
            {
                //ebug.LogWarning("[OnTriggerEnter] No Pen component found on the BrushTip object.");
            }
        }
        else
        {
           // Debug.Log("[OnTriggerEnter] Tag did not match BrushTip.");
        }
    }

}
