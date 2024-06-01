using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{

    readonly float G = 100;
    GameObject[] celestrials;
    public Rigidbody Earth;
    public Rigidbody Moon;
    // Start is called before the first frame update
    void Start()
    {
        celestrials = GameObject.FindGameObjectsWithTag("Celestials");
        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Earth.velocity = new Vector3();
            Moon.velocity = new Vector3();
            InitialVelocity();
        }
    }   

    private void FixedUpdate()
    {
        Gravity();
    }
    void Gravity()
    {
        foreach(GameObject a in celestrials)    
        {
            foreach(GameObject b in celestrials)
            {
                if(!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;

                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G* (m1 * m2) / (r*r)));

                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject a in celestrials)
        {
            foreach (GameObject b in celestrials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                }
            }
        }
    }
}
