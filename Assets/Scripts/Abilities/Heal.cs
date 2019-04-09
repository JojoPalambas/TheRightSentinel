using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public Sentinel owner;

    public GameObject effect;
    
    private float coolDown;
    private float maxCoolDown;

    private Gauge gauge;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = GameConstants.healCooldown;
        maxCoolDown = GameConstants.healCooldown;
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
        coolDown = GameConstants.healCooldown;
        maxCoolDown = GameConstants.healCooldown;

        if (effect)
        {
            Instantiate(effect, owner.turret.transform).transform.SetParent(owner.transform);
        }

        owner.Heal();
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
    }
}
