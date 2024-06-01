using UnityEngine;

public class TxtMeshRotater : MonoBehaviour
{
    private Transform cameraTransform;
    public float baseTextSize = 0.01f; // Base text size when camera is at the base distance
    void Start()
    {
        // Get the main camera's transform
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Ensure cameraTransform is not null
        if (cameraTransform == null)
            return;

        // Make the text object face the camera
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
                         cameraTransform.rotation * Vector3.up);

        // Calculate the distance between the text object and the camera
        float distanceToCamera = Vector3.Distance(transform.position, cameraTransform.position);

        // Calculate the adjusted text size based on distance
        float adjustedTextSize = baseTextSize * distanceToCamera;

        // Set the text size to the adjusted value
        transform.localScale = new Vector3(adjustedTextSize, adjustedTextSize, adjustedTextSize);
    }
}
