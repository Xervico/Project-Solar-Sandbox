using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class textUpdater : MonoBehaviour
{
    // Speed of the movement
    public float speed = 1.0f;
    public Transform Cam;
    public Transform Earth;
    public TextMeshProUGUI txt;
    public Transform Target;
    public TextMeshPro txtEarth;

    public Vector3 lockedPosition;

    // Desired size of the text in screen space
    public float desiredScreenSize = 1.0f;
    private void Start()
    {
        
        txtEarth.enabled = false;


    }
    void Update()
    {

        if (Vector3.Distance(Cam.position, Earth.position) > 500)
        {
            txtEarth.enabled = true;
        }
        else
        {
            txtEarth.enabled = false;
        }
        
        if (Cam != null && txtEarth != null)
        {
            // Calculate the distance between the camera and the text object
            float distance = Vector3.Distance(Cam.transform.position, txtEarth.transform.position);

            // Adjust the scale of the text to maintain the same screen size
            float scale = distance * desiredScreenSize;
            // Apply the scale to the RectTransform
            txtEarth.rectTransform.localScale = Vector3.one * scale;
        }

        txt.text = "Kamera Afstand fra Jorden\r\n" + Vector3.Distance(Cam.position, Earth.position) + " kilometer";

        txtEarth.transform.LookAt(Cam.transform);
        txtEarth.transform.rotation = Quaternion.LookRotation(Cam.transform.forward);
    } // Reference to the main camera





        

}
