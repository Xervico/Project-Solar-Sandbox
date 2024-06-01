using System;
using TMPro;
using UnityEngine;

public class StartEffect : MonoBehaviour
{
    public float startFOV = 179f;
    public float endFOV = 50f;
    public float caseMinusOneFOV = 50f; // FOV for case -1
    public float timeMax = 3f; // Duration of the transition in seconds

    private float elapsedTime = 0f;
    private Camera cam;

    public Transform Switcher;
    public GameObject Parenter;
    public GameObject Sun;

    public TextMeshProUGUI Perspective;
    public GameObject earthTxt;

    public int sInt = 1;
    private bool isAnimating = true;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    public bool lockCursor = true;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);

    private Vector2 _mouseAbsolute;
    private Vector2 _smoothMouse;

    public PerspectiveShift PS;
    public SceneChanger SC; // Reference to the SceneChanger script

    private void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("CameraFOVController script needs to be attached to a GameObject with a Camera component.");
        }
        else
        {
            ResetAnimation();
        }
    }

    private void Update()
    {
        if (SC.isPaused) // Check if the game is paused
        {
            // Do nothing if the game is paused
            return;
        }

        if (PS.turnOn)
        {
            if (sInt == 1)
            {
                DistanceEstimator();
                PS.turnOn = false;
            }
        }

        // Lock the cursor only when sInt == -1
        if (sInt == -1 && lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Handle camera movement
        if (sInt == -1)
        {
            HandleMouseLook();
        }

        // Handle mouse button press to switch cases
        if (Input.GetMouseButtonDown(0))
        {
            sInt = -sInt;
            HandleEvent(sInt);
        }

        // Handle FOV animation
        if (isAnimating)
        {
            if (elapsedTime < timeMax)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / timeMax; // Normalized time [0, 1]
                if (sInt == -1)
                {
                    cam.fieldOfView = caseMinusOneFOV;
                }
                else
                {
                    cam.fieldOfView = Mathf.Lerp(startFOV, endFOV, EaseOutExpo(t));
                }
            }
            else
            {
                isAnimating = false; // Stop animating when timeMax is reached
            }
        }
    }

    private void HandleEvent(int eventType)
    {
        switch (eventType)
        {
            case 1: // Start FOV animation and set camera to case 1 transform
                earthTxt.SetActive(true);
                transform.SetParent(null);
                DistanceEstimator();
                ResetAnimation();
                SetCase1Transform();
                Perspective.text = "Perspektiv - Solsystemet";
                break;

            case -1: // Lock on Sun
                ResetAnimation();
                LockOnSun();
                Perspective.text = "Perspektiv - Jorden";
                earthTxt.SetActive(false);
                break;

            default:
                Debug.LogWarning("Unhandled event type: " + eventType);
                break;
        }
    }

    private void DistanceEstimator()
    {
        switch (PS.PerNum)
        {
            case 1:
                // Set the camera's position and rotation for case 1
                cam.transform.position = new Vector3(0, 3000, 0);
                break;
            case 2:
                cam.transform.position = new Vector3(0, 10000, 0);
                break;
            case 3:
                cam.transform.position = new Vector3(0, 20000, 0);
                break;
            default:
                Debug.LogWarning("Unhandled event type: " + PS.PerNum);
                break;
        }
    }

    private void ResetAnimation()
    {
        elapsedTime = 0f;
        isAnimating = true;
    }

    private void LockOnSun()
    {
        cam.transform.position = Switcher.transform.position;
        cam.transform.parent = Parenter.transform;
        cam.transform.LookAt(Sun.transform);
    }

    private void SetCase1Transform()
    {
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private float rotationX = 0f; // Tracks the current x-axis rotation

    private void HandleMouseLook()
    {
        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Apply horizontal rotation (y-axis)
        transform.Rotate(Vector3.up * _smoothMouse.x);

        // Update and clamp vertical rotation (x-axis)
        rotationX -= _smoothMouse.y; // Reverse the vertical rotation
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply vertical rotation (x-axis) clamping
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, 0f);
    }

    private float EaseOutExpo(float t)
    {
        return t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
    }
}
