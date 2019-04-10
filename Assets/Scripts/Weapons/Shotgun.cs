using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private float maxCooldown = GameConstants.disabledGunsTime;
    private float coolDown = GameConstants.disabledGunsTime;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;

        if (gauge != null)
        {
            gauge.SetCurrentValue(coolDown);
            gauge.SetMaxValue(maxCooldown);
        }
    }

    public void Shoot(Sentinel shooter)
    {
        if (coolDown > 0)
            return;
        coolDown = GameConstants.shotgunCoolDown;
        maxCooldown = GameConstants.shotgunCoolDown;

        Instantiate(muzleFlare, transform);

        for (int i = 0; i < GameConstants.shotgunBullets; i++)
        {
            RaycastHit hitInfo;
            float range = Random.Range(GameConstants.shotgunMinRange, GameConstants.shotgunMaxRange);
            Vector3 shotDirection = Quaternion.Euler(0, Random.Range(-(360 - 360 * GameConstants.shotgunAccuracy), 360 - 360 * GameConstants.shotgunAccuracy), 0) * transform.forward;
            // If hits anything
            if (Physics.Raycast(transform.position, shotDirection, out hitInfo, range))
            {
                Instantiate(bulletImpact, hitInfo.point, Quaternion.Lerp(Quaternion.LookRotation(hitInfo.normal), Quaternion.LookRotation(-transform.forward), .5f));

                Sentinel target = hitInfo.transform.GetComponent<Sentinel>();
                if (target != null)
                    target.Hurt(owner.gameObject, GameConstants.shotgunDamage);

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
        }

        GeneralLinker.cameraManager.Shake(GameConstants.shotgunShake);
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
    }
}
