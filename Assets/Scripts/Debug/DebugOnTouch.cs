using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DebugOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched: " + other.gameObject.name);
    }
}
