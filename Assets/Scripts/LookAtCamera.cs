using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (cameraTransform != null)
        {
            // Calculate the direction towards the camera
            Vector3 lookDir = cameraTransform.position - transform.position;
            lookDir.y = 0f;

            // Rotate the object towards the camera
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
