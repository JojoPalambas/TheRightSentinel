using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;

    public Cannon cannon;
    public Shotgun shotgun;
    public MachineGun machineGun;
    public Laser laser;

    public Sentinel owner;

    // Start is called before the first frame update
    void Start()
    {
        if (cannon != null)
        {
            cannon.owner = owner;
        }
        if (shotgun != null)
        {
            shotgun.owner = owner;
        }
        if (machineGun != null)
        {
            machineGun.owner = owner;
        }
        if (laser != null)
        {
            laser.owner = owner;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot(Sentinel shooter)
    {
        if (cannon != null)
        {
            cannon.Shoot(shooter);
        }
        if (shotgun != null)
        {
            shotgun.Shoot(shooter);
        }
        if (machineGun != null)
        {
            machineGun.Shoot(shooter);
        }
        if (laser != null)
        {
            laser.Shoot(shooter);
        }
    }

    public void StopShooting()
    {
        if (laser != null)
        {
            laser.Stop();
        }
    }

    public void SetPlayerInfoDisplayer(PlayerInfoDisplayer pid)
    {
        pid.weaponName.ChangeText(weaponName);
        if (cannon != null)
        {
            cannon.SetGauge(pid.weaponCooldown);
        }
        if (shotgun != null)
        {
            shotgun.SetGauge(pid.weaponCooldown);
        }
        if (machineGun != null)
        {
            machineGun.SetGauge(pid.weaponCooldown);
        }
        if (laser != null)
        {
            laser.SetGauge(pid.weaponCooldown);
        }
    }
}
