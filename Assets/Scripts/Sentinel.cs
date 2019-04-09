using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentinel : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject turret;
    public GameObject moveDirection;
    public GameObject turretDirection;
    public Weapon weapon;
    public Ability ability;

    public Transform muzleTransform;
    public GameObject hpColorDisplayer;
    private Material hpMaterial;
    public GameObject explosion;

    private float hp = GameConstants.maxHP;
    
    private float teleportationCooldown = 0f;

    // Reminds if the sentinel's death has been scored yet
    public bool deathScored = false;

    [System.NonSerialized]
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        deathScored = false;
        hp = GameConstants.maxHP;
        hpMaterial = hpColorDisplayer.GetComponent<Renderer>().material;
    }

    public void SetPlayerInfoDisplayer(PlayerInfoDisplayer pid)
    {
        weapon.SetPlayerInfoDisplayer(pid);
        ability.SetPlayerInfoDisplayer(pid);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Decreasing cooldowns
        teleportationCooldown -= Time.deltaTime;

        if (gameManager.hpColorationEnabled)
            hpMaterial.SetColor("_Color", new Color(1, hp / GameConstants.maxHP, hp / GameConstants.maxHP));
        if (gameManager.hpRegenEnabled)
        {
            hp += GameConstants.hpRegen * Time.deltaTime;
            if (hp >= 100)
                hp = 100;
        }
    }

    public void Move(float horizontalAxis, float verticalAxis)
    {
        float moveSpeed = GameConstants.playerSpeed * GameConstants.speedModifier * Time.deltaTime;

        // Rotate moving direction
        moveDirection.transform.localPosition = new Vector3(horizontalAxis, moveDirection.transform.localPosition.y, verticalAxis);
        // Moves if direction there is an actual moving direction
        rb.MovePosition(transform.position + new Vector3(moveDirection.transform.localPosition.normalized.x * moveSpeed, 0, moveDirection.transform.localPosition.normalized.z * moveSpeed));
    }

    public void RotateTurret(float horizontalAxis, float verticalAxis)
    {
        turretDirection.transform.localPosition = new Vector3(horizontalAxis, turretDirection.transform.localPosition.y, verticalAxis);
        turret.transform.LookAt(new Vector3(turretDirection.transform.position.x, turret.transform.position.y, turretDirection.transform.position.z));
    }

    public void Shoot()
    {
        if (weapon != null)
            weapon.Shoot(this);
    }

    public void StopShooting()
    {
        if (weapon != null)
            weapon.StopShooting();
    }

    public void UseAbility()
    {
        ability.Use();
    }

    public void Hurt(GameObject killer, float damage)
    {
        if (gameManager.hpEnabled)
        {
            hp -= damage;
            if (hp <= 0f)
            {
                Die(killer);
            }
            else if (hp >= GameConstants.maxHP)
            {
                hp = GameConstants.maxHP;
            }
        }
        else
        {
            Die(killer);
        }
    }

    private void Die(GameObject killer)
    {
        gameManager.DeclareDeath(gameObject, killer);

        if (explosion)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        GeneralLinker.cameraManager.Shake(GameConstants.sentinelDeathShake);

        Destroy(gameObject);
    }

    public void Heal()
    {
        hp = GameConstants.maxHP;
    }
}
