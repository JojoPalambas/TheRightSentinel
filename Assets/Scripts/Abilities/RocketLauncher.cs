using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Sentinel owner;

    private float coolDown;
    private float maxCoolDown;
    public GameObject rocket;

    private Gauge gauge;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = GameConstants.rocketCooldown;
        maxCoolDown = GameConstants.rocketCooldown;
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
        coolDown = GameConstants.rocketCooldown;
        maxCoolDown = GameConstants.rocketCooldown;

        GameObject newRocketObject = Instantiate(rocket, owner.turret.transform.position + owner.turret.transform.forward, owner.turret.transform.rotation);
        newRocketObject.GetComponent<Rocket>().owner = owner;
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
    }
}
