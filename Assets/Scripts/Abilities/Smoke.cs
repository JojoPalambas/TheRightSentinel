using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Sentinel owner;

    public GameObject smokeEffect;

    private float coolDown;
    private float maxCoolDown;

    private Gauge gauge;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = GameConstants.smokeCooldown;
        maxCoolDown = GameConstants.smokeCooldown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coolDown -= Time.deltaTime;

        if (gauge != null)
        {
            gauge.SetCurrentValue(coolDown);
            gauge.SetMaxValue(maxCoolDown);
        }
    }

    public void Use()
    {
        if (coolDown > 0f)
            return;
        coolDown = GameConstants.smokeCooldown;
        maxCoolDown = GameConstants.smokeCooldown;

        Instantiate(smokeEffect, owner.transform.position, smokeEffect.transform.rotation);
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
    }
}
