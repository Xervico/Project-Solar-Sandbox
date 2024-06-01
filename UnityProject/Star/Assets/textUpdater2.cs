using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class textUpdater2 : MonoBehaviour
{
    // Speed of the movement
    public float speed = 1.0f;
    public Transform Cam;
    public Transform Earth;
    public TextMeshProUGUI txt;
    public Transform Target;
    public TextMeshPro txtEarth;
    public Transform sun;
    public Vector3 lockedPosition;

    // Desired size of the text in screen space
    public float desiredScreenSize = 0.01f;

    void Start()
    {
        txtEarth.enabled = false;
    }

    void Update()
    {
        // Check if the camera is within a certain distance of Earth to enable or disable the txtEarth text
        if (Vector3.Distance(Cam.position, Earth.position) > 500)
        {
            txtEarth.enabled = true;
        }
        else
        {
            txtEarth.enabled = false;
        }

        // Ensure that the camera and txtEarth are assigned
        if (Cam != null && txtEarth != null)
        {
            // Calculate the distance between the camera and the text object
            float distance = Vector3.Distance(Cam.position, txtEarth.transform.position);

            // Adjust the scale of the text to maintain the same screen size
            float scale = distance * desiredScreenSize;

            // Apply the scale to the RectTransform
            txtEarth.rectTransform.localScale = Vector3.one * scale;

        }

        // Update the txt text with the distance between the camera and Earth
        txt.text = "Camera Afstand fra Jorden r\n" + Vector3.Distance(Cam.position, Earth.position) * 10 + " million kilometer";

        // Calculate the position between Sun and Earth for txtEarth
        if (sun != null && Earth != null)
        {
            Vector3 midpoint = (sun.position + Earth.position) / 2;
            txtEarth.transform.position = midpoint;
        }
    }

}
