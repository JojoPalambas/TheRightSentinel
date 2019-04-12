using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickRegisterer : MonoBehaviour
{
    public RealInput inputs;
    public GameObject mask;
    public Vector2 playerCoordinates;
    public PlayerInfoDisplayer playerInfoDisplayer;

    public SelectButton weaponSelect;
    public SelectButton abilitySelect;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Register(RealInput realInput)
    {
        inputs = realInput;;
        mask.SetActive(false);

        FindObjectOfType<AudioManager>().Play("sound:PlayerRegister");
    }

    public void Unregister()
    {
        inputs = new RealInput();
        mask.SetActive(true);

        FindObjectOfType<AudioManager>().Play("sound:PlayerUnregister");
    }

    public void disablePlayerInfoDisplayerIfUnused()
    {
        if (inputs.joystickId == 0)
        {
            playerInfoDisplayer.gameObject.SetActive(false);
        }
    }

    public void enablePlayerInfoDisplayer()
    {
        playerInfoDisplayer.gameObject.SetActive(true);
    }
}
