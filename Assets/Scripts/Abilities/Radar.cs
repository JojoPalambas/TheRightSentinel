using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public Sentinel owner;

    public GameObject radarBubble;

    private float coolDown;
    private float maxCoolDown;

    private Gauge gauge;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = GameConstants.radarCooldown;
        maxCoolDown = GameConstants.radarCooldown;
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
        coolDown = GameConstants.radarCooldown;
        maxCoolDown = GameConstants.radarCooldown;

        RadarBubble newRadarBubble = Instantiate(radarBubble, owner.transform.position, owner.transform.rotation).GetComponent<RadarBubble>();
        newRadarBubble.setRadarZoneOwner(owner);
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
    }
}
