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
            Debug.LogError("No Renderer found on " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        Debug.Log("Eimai sto onCollision me " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("BrushTip"))
        {
            Debug.Log("Eimai sto onCollision");
            Pen pen = collision.gameObject.transform.GetComponent<Pen>();
            if (pen != null)
            {
                Debug.Log("Kalw to switch " + color.ToString());
                pen.SwitchColor(color);
            }
        }
    }
}
