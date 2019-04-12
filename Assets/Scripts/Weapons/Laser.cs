using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float maxCooldown = GameConstants.disabledGunsTime;
    private float coolDown = GameConstants.disabledGunsTime;

    //public GameObject muzleFlare;
    public GameObject impact;
    private ParticleSystem impactParticles;
    public LineRenderer laserRenderer;

    public Sentinel owner;

    private Gauge gauge;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = GameConstants.disabledGunsTime;
        impact = Instantiate(impact);
        impact.transform.SetParent(transform);
        impactParticles = impact.GetComponent<ParticleSystem>();
        impactParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        laserRenderer.SetPosition(0, transform.position);

        if (gauge != null)
        {
            gauge.SetCurrentValue(coolDown);
            gauge.SetMaxValue(maxCooldown);
        }
    }

    public void Shoot(Sentinel shooter)
    {
        if (coolDown > 0f)
        {
            Stop();
            return;
        }

        // If no sound is playing, play sound
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }

        RaycastHit hitInfo;
        float range = GameConstants.laserRange;
        // If hits anything
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range))
        {
            Sentinel target = hitInfo.transform.GetComponent<Sentinel>();
            if (target != null)
            {
                target.Hurt(owner.gameObject, GameConstants.laserDamagePerSecond * Time.deltaTime);
            }

            SetLaserEnd(hitInfo.point, true);
        }
        else
        {
            SetLaserEnd(transform.position + transform.forward * GameConstants.laserRange, true);
            // Move the particles to the target
        }
    }

    private void SetLaserEnd(Vector3 position, bool active)
    {
        laserRenderer.SetPosition(1, position);
        impact.transform.position = position;
        if (active)
        {
            //impactParticles.Play();
            ParticleSystem.EmissionModule em = impactParticles.emission;
            em.enabled = true;
        }
        else if (!active)
        {
            //impactParticles.Stop();
            ParticleSystem.EmissionModule em = impactParticles.emission;
            em.enabled = false;
        }
    }

    public void Stop()
    {
        SetLaserEnd(transform.position, false);
        
        // If sound is playing, stop sound
        if (GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
    }

    public void SetGauge(Gauge gauge)
    {
        this.gauge = gauge;
    }
}
