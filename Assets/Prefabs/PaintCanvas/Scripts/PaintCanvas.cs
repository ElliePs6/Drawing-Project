using UnityEngine;

public class PaintCanvas : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(x: 2048, y: 2048);


    void Start()
    {

        var r = GetComponent<Renderer>();
        texture = new Texture2D(width: (int)textureSize.x, height: (int)textureSize.y);
        r.material.mainTexture = texture;

    }

    void OnCollisionEnter(Collision collision)
{
    Debug.Log("Collided with: " + collision.gameObject.name);
}

}

