using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    private float maxCooldown = GameConstants.disabledGunsTime;
    private float coolDown = GameConstants.disabledGunsTime;
    private float reloadingTime;
    private int bullets;
    private bool firing;

    private Sentinel shooter;

    public GameObject muzleFlare;
    public GameObject bulletImpact;
    public GameObject bulletMiss;
    public GameObject bulletLine;

    public Sentinel owner;

    private Gauge gauge;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = GameConstants.disabledGunsTime;
        reloadingTime = 0f;
        bullets = GameConstants.machineGunReloadBullets;
        firing = false;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        reloadingTime -= Time.deltaTime;

        if (firing)
        {
            ActuallyShoot(shooter);
        }

        if (gauge != null)
        {
            gauge.SetCurrentValue(reloadingTime);
            gauge.SetMaxValue(maxCooldown);
        }
    }

    public void Shoot(Sentinel shooter)
    {
        this.shooter = shooter;
        if (coolDown <= 0f && reloadingTime <= 0)
        {
            firing = true;
        }
    }

    public void ActuallyShoot(Sentinel shooter)
    {
        this.shooter = shooter;
        if (bullets <= 0)
        {
            firing = false;
            bullets = GameConstants.machineGunReloadBullets;
            reloadingTime = GameConstants.machineGunReloadingDuration;
            maxCooldown = GameConstants.machineGunReloadingDuration;
        }
        if (coolDown > 0)
        {
            return;
        }

        coolDown = GameConstants.machineGunCoolDown;
        bullets -= 1;

        Instantiate(muzleFlare, transform);

        RaycastHit hitInfo;
        float range = Random.Range(GameConstants.machineGunMinRange, GameConstants.machineGunMaxRange);
        Vector3 shotDirection = Quaternion.Euler(0, Random.Range(-(360 - 360 * GameConstants.machineGunAccuracy), 360 - 360 * GameConstants.machineGunAccuracy), 0) * transform.forward;
        // If hits anything
        if (Physics.Raycast(transform.position, shotDirection, out hitInfo, range))
        {
            Instantiate(bulletImpact, hitInfo.point, Quaternion.Lerp(Quaternion.LookRotation(hitInfo.normal), Quaternion.LookRotation(-transform.forward), .5f));

            Sentinel target = hitInfo.transform.GetComponent<Sentinel>();
            if (target != null)
                target.Hurt(owner.gameObject, GameConstants.machineGunDamage);
        }
        else
        {
            Instantiate(bulletMiss, transform.position + shotDirection.normalized * range, transform.rotation);
        }

        GeneralLinker.cameraManager.Shake(GameConstants.machineGunShake);
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
    }
}
