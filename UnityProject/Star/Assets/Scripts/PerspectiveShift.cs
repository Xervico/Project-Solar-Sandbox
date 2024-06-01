using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;
using UnityEngine.Experimental.GlobalIllumination;

public class PerspectiveShift : MonoBehaviour
{
    public int PerNum = 1;

    float disEM = 3;
    float disSE = 1510;

    public Light PL;
    public GameObject Earth;
    public GameObject Moon;
    public Rigidbody Sun;

    float G = 100; // Gravitational constant from the code
    float sunMass = 333000; // Mass of the Sun
    float earthMass = 1; // Mass of the Earth
    // Start is called before the first frame update
    public TextMeshProUGUI SunDis;
    public TextMeshProUGUI MoonDis;
    public TextMeshProUGUI RotationDays;
    public TextMeshProUGUI View;
    public TextMeshProUGUI Ratio;

    public SceneChanger SC;
    void Start()
    {
        CalculateOrbitalPeriod();
        SwitchHandler();
    }

    public bool turnOn = false;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            //if (SC.isPaused == false)
            //{
                if (PerNum >= 3)
                {

                    PerNum = 1;
                    SwitchHandler();
                }
                else
                {
                    PerNum++;
                    SwitchHandler();
                }
                turnOn = true;
            //}
        }
        
        
    }
    void DistanceMaker()
    {

        View.text = "visningstilstand - nr." + PerNum.ToString();
        SunDis.text = "Afstand fra Solen - " + disSE.ToString() ;
        MoonDis.text = "Afstand fra Månen - " + disEM.ToString();
        Earth.transform.position = new Vector3(disSE, 0, 0);
        Moon.transform.localPosition = new Vector3(disEM, 0, 0);
        CalculateOrbitalPeriod();
    }
    public int ratioCon = 1;
    int intensCon = 5;
    void SwitchHandler()
    {
        switch (PerNum)
        {
            
            case 1: // Start FOV animation and set camera to case 1 transform
                ratioCon = 1;
                disEM = 3 * ratioCon;
                disSE = 1510 * ratioCon;
                DistanceMaker();
                
                Ratio.text = "Ratio - 1:" + ratioCon.ToString();
                PL.intensity = 1000000 * ratioCon;
                Sun.mass = 333000;
                
                break;
            case 2: // Lock on Sun
                ratioCon = 5;
                disEM = 3 * ratioCon;
                disSE = 1510 * ratioCon;
                DistanceMaker();
                Ratio.text = "Ratio - 1:" +ratioCon.ToString();
                PL.intensity = 1000000 * ratioCon * intensCon;
                Sun.mass = 333000 * ratioCon;
                break;
            case 3:
                ratioCon = 10;
                disEM = 3 * ratioCon;
                disSE = 1510 * ratioCon;
                Ratio.text = "Ratio - 1:" + ratioCon.ToString();
                PL.intensity = 1000000 * ratioCon * intensCon;
                Sun.mass = 333000 * ratioCon;
                DistanceMaker();
                break;
            default:
                Debug.LogWarning("Unhandled event type: " + PerNum);
                break;
        }
    }
    void CalculateOrbitalPeriod()
    {
        float G = 100; // Gravitational constant from the code
        float sunMass = 333000; // Mass of the Sun
        float earthMass = 1; // Mass of the Earth
        float initialVelocity = Mathf.Sqrt(G * sunMass / 1510); // Initial velocity calculation from the code

        // Calculate orbital period using Kepler's third law
        float orbitalPeriod = Mathf.Sqrt((4 * Mathf.PI * Mathf.PI * disSE * disSE * disSE) / (G * (sunMass + earthMass)));

        RotationDays.text = "En rotation tager: " + orbitalPeriod.ToString() + " sekunder!";
    }
}
