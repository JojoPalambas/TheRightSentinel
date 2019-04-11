using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameConstants
{
    // Weapons

    public static readonly float cannonCoolDown = .75f;
    public static readonly float cannonMinRange = 90f;
    public static readonly float cannonMaxRange = 110f;
    public static readonly float cannonAccuracy = .99f;
    public static readonly float cannonShake = .5f;
    public static readonly int cannonDamage = 60;

    public static readonly float machineGunCoolDown = .08f;
    public static readonly float machineGunReloadingDuration = .5f;
    public static readonly int machineGunReloadBullets = 3;
    public static readonly float machineGunMinRange = 15f;
    public static readonly float machineGunMaxRange = 16f;
    public static readonly float machineGunAccuracy = .985f;
    public static readonly float machineGunShake = .25f;
    public static readonly int machineGunDamage = 20;

    public static readonly float shotgunCoolDown = 1f;
    public static readonly int shotgunBullets = 9;
    public static readonly float shotgunMinRange = 9f;
    public static readonly float shotgunMaxRange = 11f;
    public static readonly float shotgunAccuracy = .95f;
    public static readonly float shotgunShake = .5f;
    public static readonly int shotgunDamage = 10;
    
    public static readonly float laserRange = 100f;
    public static readonly int laserDamagePerSecond = 80;

    // Abilities

    public static readonly float teleportationRange = 15f;
    public static readonly float teleportationCooldown = 15f;

    public static readonly int healSpeed = 100;
    public static readonly float healCooldown = 15f;

    public static readonly float rocketLifetime = 15f;
    public static readonly float rocketCooldown = 20f;
    public static readonly float rocketSpeed = 20f;
    public static readonly float rocketExplosionRadius = 5f;
    public static readonly float rocketDamage = 75f;
    public static readonly float rocketShake = 5f;
    
    public static readonly float radarCooldown = 15f;
    public static readonly float radarBubbleGrowingSpeed = 15f;
    public static readonly int radarBubbleParticlesIntensity = 10;
    public static readonly int radarBubbleMaxRadius = 100;

    public static readonly float smokeCooldown = 15f;

    // General

    public static readonly float disabledGunsTime = 5f;
    public static readonly float untransparencingSpeed = .1f;

    public static readonly float speedModifier = 5f;
    public static readonly float playerSpeed = 1f;
    public static readonly float moveInputTolerance = 0.1f;

    public static readonly int maxHP = 100;
    public static readonly float hpRegen = 10; // HP per second
    public static readonly float sentinelDeathShake = 1f;

    public static readonly int playerValue = 100;
    public static readonly int citizenValue = 5;

    public static readonly int endGameScore = 1000;

    public static readonly string[] weapons =
    {
        "Cannon",
        "Machine gun",
        "Shotgun",
        "Laser",
    };

    public static readonly string[] abilities =
    {
        "Teleportation",
        "Rocket launcher",
        "Smoke",
        "Heal",
        "Radar",
    };
}
