using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rotation : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 5f; // An extremely high value

    void Update()
    {


        // Rotate the GameObject around its local Y axis at the specified speed
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
