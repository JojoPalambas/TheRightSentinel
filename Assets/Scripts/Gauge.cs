using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    // The two RectTransform MUST have the same width, and be centered on x = 0 when the gauge is full!
    public RectTransform maskTransform;
    public RectTransform gaugeTransform;

    private float maxValue = 0;
    private float currentValue = 0;

    private float width;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        width = maskTransform.rect.width;
        height = maskTransform.rect.height;

        SetCurrentValue(0);
        SetMaxValue(10);
    }

    // Update is called once per frame
    void Update()
    {
        // If MaxValue == 0, no cooldown, the gauge appears always full
        if (maxValue <= 0)
            gaugeTransform.localPosition = new Vector3(0, gaugeTransform.localPosition.y, gaugeTransform.localPosition.z);
        else
        {
            gaugeTransform.localPosition = new Vector3(-width + (currentValue / maxValue) * width, gaugeTransform.localPosition.y, gaugeTransform.localPosition.z);
        }
    }

    public void SetMaxValue(float n)
    {
        if (n < 0)
            n = 0;

        maxValue = n;
    }

    public void SetCurrentValue(float n)
    {
        if (n < 0)
            n = 0;
        if (n > maxValue)
            n = maxValue;

            currentValue = n;
    }
}
