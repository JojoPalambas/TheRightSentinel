using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLine : MonoBehaviour
{
    private float startAlpha;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public LineRenderer lineRenderer;
    public float disappearingTime;

    // Start is called before the first frame update
    void Start()
    {
        startAlpha = lineRenderer.startColor.a;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // Decreasing the alpha
        float newAlpha = lineRenderer.startColor.a - (Time.deltaTime * startAlpha / disappearingTime);
        if (newAlpha < 0)
            Destroy(gameObject);
        Color newColor = new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, newAlpha);
        lineRenderer.startColor = newColor;
        lineRenderer.endColor = newColor;
    }
}
