using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    public string contentType;
    private string[] values;
    public int currentValueIndex;
    public TextDisplayer textDisplayer;

    // Start is called before the first frame update
    void Start()
    {
        if (contentType == "Weapons")
            values = GameConstants.weapons;
        else if (contentType == "Abilities")
            values = GameConstants.abilities;
        else
            values = GameConstants.weapons;

        currentValueIndex = 0;
        textDisplayer.ChangeText(values[currentValueIndex]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Click()
    {
        currentValueIndex++;
        if (currentValueIndex >= values.Length)
        {
            currentValueIndex = 0;
        }
        textDisplayer.ChangeText(values[currentValueIndex]);
    }

    public string GetCurrentValue()
    {
        return values[currentValueIndex];
    }
}
