using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Transform earthTransform;
    public LineRenderer lineRenderer;
    public float trailDuration = 9000f; // Duration of the trail in seconds
    private Queue<TimedPosition> positions = new Queue<TimedPosition>();
    public Color startColor = new Color32(0x57, 0xB1, 0xF1, 0x80); // Light blue with transparency
    public Color endColor = new Color32(0x3F, 0x67, 0xB2, 0x80); // Dark blue with transparency
    public float sWidth = 8f;
    public float eWidth = 5f;

    public PerspectiveShift PS;
    [System.Serializable]
    private struct TimedPosition
    {
        public Vector3 position;
        public float time;

        public TimedPosition(Vector3 position, float time)
        {
            this.position = position;
            this.time = time;
        }
    }

    void Start()
    {
        // Ensure the Line Renderer has a valid material with a shader that supports transparency
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the start and end colors of the Line Renderer
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = sWidth;
        lineRenderer.endWidth = eWidth;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ResetTrail();
            TrailWidth();
        }
        float currentTime = Time.time;

        // Add current position with timestamp
        positions.Enqueue(new TimedPosition(earthTransform.position, currentTime));

        // Remove positions that are older than trailDuration
        while (positions.Count > 0 && currentTime - positions.Peek().time > trailDuration)
        {
            positions.Dequeue();
        }

        // Update the Line Renderer
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(GetPositionsArray());

    }
    private void TrailWidth()
    {
        /*
        switch (PS.PerNum)
        {

            case 1:
                // Set the camera's position and rotation for case 1
                lineRenderer.startWidth = sWidth * PS.ratioCon;
                lineRenderer.endWidth = eWidth * PS.ratioCon;
                break;
            case 2:
                lineRenderer.startWidth = sWidth * PS.ratioCon;
                lineRenderer.endWidth = eWidth * PS.ratioCon;
                break;
            case 3:
                lineRenderer.startWidth = sWidth * PS.ratioCon;
                lineRenderer.endWidth = eWidth * PS.ratioCon;
                break;
            default:
                Debug.LogWarning("Unhandled event type: " + PS.PerNum);
                break;
        }
        */
    }
    public void ResetTrail()
    {
        // Clear the positions queue
        positions.Clear();

        // Reset the Line Renderer
        lineRenderer.positionCount = 0;
    }
    private Vector3[] GetPositionsArray()
    {
        Vector3[] positionsArray = new Vector3[positions.Count];
        int index = 0;
        foreach (var timedPosition in positions)
        {
            positionsArray[index] = timedPosition.position;
            index++;
        }
        return positionsArray;
    }
}
