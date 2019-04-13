using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    private float maxCooldown = GameConstants.disabledGunsTime;
    private float coolDown = GameConstants.disabledGunsTime;
    private int bullets;

    private Sentinel shooter;

    public GameObject muzleFlare;
    public GameObject bulletImpact;
    public GameObject bulletMiss;
    public GameObject bulletLine;

    public Sentinel owner;

    private Gauge gauge;
    private bool displayCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        maxCooldown = GameConstants.disabledGunsTime;
        coolDown = GameConstants.disabledGunsTime;

        bullets = GameConstants.machineGunReloadBullets;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        if (gauge != null)
            gauge.SetCurrentValue(coolDown);

        if (coolDown <= 0f)
        {
            if (bullets <= 0)
            {
                bullets = 3;
                coolDown = GameConstants.machineGunReloadingDuration;

                if (gauge != null)
                {
                    gauge.SetCurrentValue(coolDown);
                    gauge.SetMaxValue(coolDown);
                }
            }
            else if (bullets == GameConstants.machineGunReloadBullets)
            {
                // Do nothing, wait for shoot
            }
            else
            {
                Fire(shooter);
            }
        }
    }

    public void Shoot(Sentinel shooter)
    {
        // If the rafale is at its beginning, starts a new rafale
        if (bullets >= GameConstants.machineGunReloadBullets & coolDown <= 0f)
        {
            Fire(shooter);
        }
    }

    public void Fire(Sentinel shooter)
    {
        this.shooter = shooter;

        bullets--;
        coolDown = GameConstants.machineGunCoolDown;
        maxCooldown = GameConstants.machineGunCoolDown;

        if (gauge != null)
        {
            gauge.SetCurrentValue(GameConstants.machineGunCoolDown);
            gauge.SetMaxValue(GameConstants.machineGunCoolDown);
        }

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

            if (bulletLine != null)
            {
                BulletLine bl = Instantiate(bulletLine, new Vector3(), new Quaternion()).GetComponent<BulletLine>();
                bl.startPosition = transform.position;
                bl.endPosition = hitInfo.point;
            }
        }
        else
        {
            Instantiate(bulletMiss, transform.position + shotDirection.normalized * range, transform.rotation);

            if (bulletLine != null)
            {
                BulletLine bl = Instantiate(bulletLine, new Vector3(), new Quaternion()).GetComponent<BulletLine>();
                bl.startPosition = transform.position;
                bl.endPosition = transform.position + shotDirection.normalized * range;
            }
        }

        GeneralLinker.cameraManager.Shake(GameConstants.machineGunShake);
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
        gauge.SetCurrentValue(coolDown);
        gauge.SetMaxValue(coolDown);
    }
}
