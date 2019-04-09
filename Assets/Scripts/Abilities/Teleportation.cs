using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Sentinel owner;

    public GameObject inEffect;
    public GameObject outEffect;

    private float coolDown;
    private float maxCoolDown;

    private Gauge gauge;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = GameConstants.teleportationCooldown;
        maxCoolDown = GameConstants.teleportationCooldown;
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
        coolDown = GameConstants.teleportationCooldown;
        maxCoolDown = GameConstants.teleportationCooldown;

        if (inEffect)
            Instantiate(inEffect, owner.turret.transform);

        RaycastHit hitInfo;
        Vector3 target;
        // If hits anything, do something
        if (Physics.Raycast(owner.turret.transform.position, owner.turret.transform.forward, out hitInfo, GameConstants.teleportationRange))
        {
            // Finds the teleportation target
            target = hitInfo.point;
        }
        else
        {
            // Finds the teleportation target
            target = owner.transform.position + owner.turret.transform.forward.normalized * GameConstants.teleportationRange;
        }
        // Teleports to the target -1 meter
        owner.transform.position = target - (target - owner.transform.position).normalized;

        if (outEffect != null)
            Instantiate(outEffect, owner.turret.transform);
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
    }
}
