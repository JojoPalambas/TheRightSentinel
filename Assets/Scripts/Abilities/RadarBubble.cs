using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBubble : MonoBehaviour
{
    public ParticleSystem particleCircle;

    public GameObject radarZone;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Radar zone management

        radarZone.transform.localScale += new Vector3(1, 1, 1) * GameConstants.radarBubbleGrowingSpeed * Time.deltaTime * 2;

        // Particles management

        ParticleSystem.ShapeModule particleShape = particleCircle.shape;
        ParticleSystem.EmissionModule particleEmission = particleCircle.emission;

        particleShape.radius += GameConstants.radarBubbleGrowingSpeed * Time.deltaTime;
        if (particleShape.radius > GameConstants.radarBubbleMaxRadius)
            Destroy(gameObject);
        particleEmission.rateOverTime = particleShape.radius * particleShape.radius * GameConstants.radarBubbleParticlesIntensity;

        // If the bubble approaches its end, fades the sound effect out
        if (particleShape.radius > GameConstants.radarBubbleMaxRadius * .75f)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.volume -= audioSource.volume / 10;
        }
    }

    public void setRadarZoneOwner(Sentinel owner)
    {
        radarZone.GetComponent<RadarZone>().owner = owner;
    }
}
