using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Switcher : MonoBehaviour
{
    public float speed = 0.2f;
    public Transform Cam;

    public Transform TargetSun;
    public Transform TargetEarth;
    // Start is called before the first frame update

    public bool defaultview = true;
    public bool defaultview2 = false;
    // Velocity used internally by SmoothDamp
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 2.5f;
    private void Start()
    {
        Debug.Log(Vector3.Distance(TargetSun.position, TargetEarth.position));
    }
    private void Update()
    {
        if(defaultview2 == true)
        {
            if (defaultview == false)
            {
                // Calculate the new position
                Vector3 newPosition = Vector3.SmoothDamp(Cam.transform.position, TargetSun.transform.position, ref velocity, smoothTime);

                Cam.transform.position = newPosition;
            }
            else
            {
                // Calculate the new position
                Vector3 newPosition = Vector3.SmoothDamp(Cam.transform.position, TargetEarth.transform.position, ref velocity, smoothTime);

                Cam.transform.position = newPosition;

            }
        }

    }
    public void ButtonPress()
    {
        defaultview2 = true;
        if (defaultview == false)
        {
            defaultview = true;
        }
        else
        {
            defaultview = false;
        }

    }


}
