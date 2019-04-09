using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sentinel sentinel;
    public RealInput input;

    [Space]
    public int id;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sentinel.Move(Input.GetAxis(input.horizontal), Input.GetAxis(input.vertical));
        sentinel.RotateTurret(Input.GetAxis(input.turretHorizontal), Input.GetAxis(input.turretVertical));

        if (Input.GetAxis(input.fire) != 0f)
            sentinel.Shoot();
        else
            sentinel.StopShooting();

        if (Input.GetAxis(input.ability) != 0f)
            sentinel.UseAbility();
    }

    public void SetPlayerInfoDisplayer(PlayerInfoDisplayer pid)
    {
        sentinel.SetPlayerInfoDisplayer(pid);
    }
}
