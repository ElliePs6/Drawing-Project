using UnityEngine;

public class BrushSmooth : MonoBehaviour
{
  
        public Transform targetHand; 
        public float smoothTime = 0.05f;

        private Vector3 velocity;
        private Quaternion rotationVelocity;

        void Update()
        {
           /** transform.position = Vector3.SmoothDamp(transform.position, targetHand.position, ref velocity, smoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetHand.rotation, Time.deltaTime / smoothTime);

            /**This keeps rotation interpolation smooth even if the framerate varies.**/
            if (smoothTime <= 0f) smoothTime = 0.01f;

                transform.position = Vector3.SmoothDamp(transform.position, targetHand.position, ref velocity, smoothTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetHand.rotation, Mathf.Clamp01(Time.deltaTime / smoothTime));
    }
    

}
