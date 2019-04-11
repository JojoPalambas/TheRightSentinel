using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RealInput
{
    public int joystickId;
    public bool alreadyInUse;

    public string validate;
    public string cancel;
    public string horizontal;
    public string vertical;
    public string turretHorizontal;
    public string turretVertical;
    public string fire;
    public string ability;

    /*
    public RealInput()
    {
        joystickId = 0;
        alreadyInUse = false;

        validate = "";
        cancel = "";
        horizontal = "";
        vertical = "";
        turretHorizontal = "";
        turretVertical = "";
        fire = "";
        ability = "";
    }*/
}

public class SelectMenu : MonoBehaviour
{
    public List<RealInput> realInputs;
    public JoystickRegisterer[] playerRegisterers;

    public Button playButton;

    public Toggle hpToggle;
    public Toggle hpColorationToggle;
    public Toggle hpRegenToggle;
    public Toggle aiSyncToggle;

    // Start is called before the first frame update
    void Start()
    {
        foreach (RealInput realInput in realInputs)
        {
            realInput.alreadyInUse = false;
        }
        CheckGameConditions();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayersRegistration();
        CheckPlayersUnregistration();
    }

    private void CheckPlayersRegistration()
    {
        foreach (RealInput realInput in realInputs)
        {
            // If an input clicks
            if (Input.GetAxisRaw(realInput.validate) != 0f)
            {
                RegisterPlayer(realInput);
            }
        }
    }

    private void RegisterPlayer(RealInput realInput)
    {
        // If the input isn't used by any player yet
        if (!realInput.alreadyInUse)
        {
            // Assigns the input to the first unregistered registerer
            foreach (JoystickRegisterer playerRegisterer in playerRegisterers)
            {
                if (playerRegisterer.inputs.joystickId == 0)
                {
                    realInput.alreadyInUse = true;
                    playerRegisterer.Register(realInput);
                    break;
                }
            }
        }
        CheckGameConditions();
    }

    private void CheckPlayersUnregistration()
    {
        foreach (RealInput realInput in realInputs)
        {
            // If an input clicks
            if (Input.GetAxisRaw(realInput.cancel) != 0f)
            {
                UnregisterPlayer(realInput);
            }
        }
    }

    private void UnregisterPlayer(RealInput realInput)
    {
        // Finds the registered player
        foreach (JoystickRegisterer playerRegisterer in playerRegisterers)
        {
            // If this input is registered in the player registerer
            if (playerRegisterer.inputs.joystickId == realInput.joystickId)
            {
                realInput.alreadyInUse = false;
                playerRegisterer.Unregister();
                break;
            }
        }
        CheckGameConditions();
    }

    // True if the game can be started
    private void CheckGameConditions()
    {
        int playerCount = 0;
        foreach (JoystickRegisterer registerer in playerRegisterers)
        {
            if (registerer.inputs.joystickId != 0)
            {
                playerCount++;
            }
        }
        playButton.interactable = playerCount >= 2;
    }
}
