using UnityEngine;

public class PalletteColors : MonoBehaviour
{
    private Color color;
    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Get the base color explicitly
            if (renderer.material.HasProperty("_BaseColor"))
            {
                color = renderer.material.GetColor("_BaseColor");
                Debug.Log("Color code: " + color.ToString());
            }
            else
            {
                Debug.LogWarning("Material does not have _BaseColor property.");
            }
        }
        else
        {
            Debug.LogError("No Renderer found on " + gameObject.name);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
       Debug.Log($"[OnTriggerEnter] Triggered by: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("BrushTip"))
        {
            Pen pen = collision.gameObject.GetComponent<Pen>();
        if (collision.gameObject.name == "Tip" )
          {
               pen = collision.gameObject.transform.parent.GetComponent<Pen>();
          }
        
            Debug.Log("[OnTriggerEnter] BrushTip tag matched.");

        

            if (pen != null)
            {
                Debug.Log("[OnTriggerEnter] Pen component found. Switching color...");
                pen.SwitchColor(color);
            }
            else
            {
                Debug.LogWarning("[OnTriggerEnter] No Pen component found on the BrushTip object.");
            }
        }
        else
        {
           Debug.Log("[OnTriggerEnter] Tag did not match BrushTip.");
        }
    }

}
