using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Sentinel owner;

    public Light lightEmitter;
    public ParticleSystem smokeSystem;
    public ParticleSystem explosion;

    public SphereCollider rocketCollider;

    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasExploded)
            transform.position += transform.forward * GameConstants.rocketSpeed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!hasExploded)
            Explode();
    }

    public void Explode()
    {
        hasExploded = true;
        smokeSystem.Stop();
        lightEmitter.intensity = 0;
        rocketCollider.enabled = false;

        // Stopping thruster sound
        GetComponent<AudioSource>().Stop();

        if (explosion)
            Instantiate(explosion, transform.position, explosion.transform.rotation);
        GeneralLinker.cameraManager.Shake(GameConstants.rocketShake);

        Collider[] victimColliders = Physics.OverlapSphere(transform.position, GameConstants.rocketExplosionRadius);
        foreach (Collider victimCollider in victimColliders)
        {
            Sentinel victim = victimCollider.GetComponent<Sentinel>();
            if (victim != null)
            {
                victim.Hurt(owner.gameObject, GameConstants.rocketDamage);
            }
        }

        Destroy(gameObject, 15f);
    }
}
