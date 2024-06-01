using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLine : MonoBehaviour
{
    public Transform sun;
    public Transform earth;
    public LineRenderer lineRenderer;
    public TextMeshProUGUI distanceText;
    public Camera mainCamera;

    // Width parameters
    public float minWidth = 1.0f;
    public float maxWidth = 20.0f;

    void Update()
    {
        if (sun != null && earth != null && lineRenderer != null && distanceText != null && mainCamera != null)
        {
            // Calculate the distance between Earth and the main camera
            float distanceToCamera = Vector3.Distance(earth.position, mainCamera.transform.position);

            // Calculate the width based on the distance
            float lineWidth = Mathf.Lerp(minWidth, maxWidth, Mathf.InverseLerp(0f, 1000f, distanceToCamera));
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            // Set the positions of the LineRenderer
            lineRenderer.SetPosition(0, sun.position);
            lineRenderer.SetPosition(1, earth.position);

            // Calculate the distance between the sun and earth
            float distance = Vector3.Distance(sun.position, earth.position);

            // Display the distance on the TextMeshPro component
            distanceText.text = "Distance: " + distance.ToString("F2") + " AU"; // Assuming distance is in Astronomical Units

            // Calculate the position of the text along the line
            Vector3 midPoint = (sun.position + earth.position) / 2;
            Vector3 textPosition = midPoint;

            // Calculate the position of the left side of the screen in world space
            Vector3 leftSideOfScreen = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, mainCamera.nearClipPlane));

            // Calculate the distance between the earth and the left side of the screen
            float distanceToLeftSide = Mathf.Clamp(leftSideOfScreen.x - earth.position.x, 0f, float.MaxValue);

            // Offset the text position based on the distance to the left side of the screen
            textPosition.x -= distanceToLeftSide;

            // Set the position of the text
            distanceText.transform.position = textPosition;
        }
    }
}
