using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenIdle : MonoBehaviour
{
    public Sentinel sentinel;

    public bool apocalypseMode;
    [Range(0, 1)]
    public float apocalypseProbability;

    // AI variables
    private float decisionCooldown = 0f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private float horizontalTurret = 0f;
    private float verticalTurret = 0f;

    // Start is called before the first frame update
    void Start()
    {
        TakeDecision();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        decisionCooldown -= Time.deltaTime;
        if (decisionCooldown <= 0f)
            TakeDecision();

        sentinel.Move(horizontalMove, verticalMove);
        sentinel.RotateTurret(horizontalTurret, verticalTurret);
    }

    private void TakeDecision()
    {
        decisionCooldown = Random.value * 2 + 0.5f;

        horizontalMove = (int) (Random.value * 3) - 1;
        verticalMove = (int) (Random.value * 3) - 1;
        horizontalTurret = (int) (Random.value * 3) - 1;
        verticalTurret = (int) (Random.value * 3) - 1;

        if (apocalypseMode)
        {
            if (Random.value < apocalypseProbability)
            {
                sentinel.Shoot();
            }
        }
    }
}
