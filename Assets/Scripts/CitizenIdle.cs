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
        if (sentinel.gameManager.aiSync)
            decisionCooldown = GameConstants.syncTimeForAIDecision;
        else
            decisionCooldown = Random.value * GameConstants.maxTimeForAIDecision + GameConstants.minTimeForAIDecision;

        horizontalMove = (Random.value * 3) - 1.5f;
        verticalMove = (Random.value * 3) - 1.5f;
        horizontalTurret = (Random.value * 3) - 1.5f;
        verticalTurret = (Random.value * 3) - 1.5f;

        if (apocalypseMode)
        {
            if (Random.value < apocalypseProbability)
            {
                sentinel.Shoot();
            }
        }
    }
}
