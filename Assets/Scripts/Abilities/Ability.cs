using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string abilityName;

    public Teleportation teleportation;
    public Heal heal;
    public RocketLauncher rocketLauncher;
    public Radar radar;
    public Smoke smoke;

    public Sentinel owner;

    // Start is called before the first frame update
    void Start()
    {
        if (teleportation != null)
        {
            teleportation.owner = owner;
        }
        if (heal != null)
        {
            heal.owner = owner;
        }
        if (rocketLauncher != null)
        {
            rocketLauncher.owner = owner;
        }
        if (radar != null)
        {
            radar.owner = owner;
        }
        if (smoke != null)
        {
            smoke.owner = owner;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Use()
    {
        if (teleportation != null)
        {
            teleportation.Use();
        }
        if (heal != null)
        {
            heal.Use();
        }
        if (rocketLauncher != null)
        {
            rocketLauncher.Use();
        }
        if (radar != null)
        {
            radar.Use();
        }
        if (smoke != null)
        {
            smoke.Use();
        }
    }

    public void SetPlayerInfoDisplayer(PlayerInfoDisplayer pid)
    {
        pid.abilityName.ChangeText(abilityName);

        if (teleportation != null)
        {
            teleportation.SetGauge(pid.abilityCooldown);
        }
        if (heal != null)
        {
            heal.SetGauge(pid.abilityCooldown);
        }
        if (rocketLauncher != null)
        {
            rocketLauncher.SetGauge(pid.abilityCooldown);
        }
        if (radar != null)
        {
            radar.SetGauge(pid.abilityCooldown);
        }
        if (smoke != null)
        {
            smoke.SetGauge(pid.abilityCooldown);
        }
    }
}
