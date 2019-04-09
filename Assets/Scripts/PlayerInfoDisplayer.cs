using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoDisplayer : MonoBehaviour
{
    public TextDisplayer playerName;
    public TextDisplayer score;
    public TextDisplayer weaponName;
    public Gauge weaponCooldown;
    public TextDisplayer abilityName;
    public Gauge abilityCooldown;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setPlayerName(string name)
    {
        playerName.ChangeText(name);
    }
}
