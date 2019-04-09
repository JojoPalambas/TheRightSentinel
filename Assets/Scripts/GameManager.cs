using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum ProgramStatus
    {
        mainMenu,
        selectMenu,
        scoresMenu,
        game
    }

    public GameObject citizenPrefab;
    public GameObject playerPrefab;

    public GameObject defaultWeapon;
    public List<GameObject> weapons;
    public GameObject defaultAbility;
    public List<GameObject> abilities;

    public List<PlayerInfoDisplayer> playerInfoDisplayers;
    public ScoresDisplay scoresMenuDisplay;
    public SelectMenu selectMenu;

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> citizens = new List<GameObject>();

    private int[] playerScores = new int[4];

    public bool hpEnabled = true;
    public bool hpColorationEnabled = true;
    public bool hpRegenEnabled = true;

    private ProgramStatus programStatus = ProgramStatus.mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        SetProgramStatus(ProgramStatus.selectMenu);
        foreach (string joystick in Input.GetJoystickNames())
        {
            Debug.Log(joystick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (programStatus == ProgramStatus.selectMenu)
            {
                Application.Quit();
            }
            else
            {
                CleanRound();
                SetProgramStatus(ProgramStatus.selectMenu);
            }
            return;
        }
        if (programStatus == ProgramStatus.mainMenu)
        {
            // Do nothing
        }
        else if (programStatus == ProgramStatus.selectMenu)
        {
            // Do nothing
        }
        else if (programStatus == ProgramStatus.scoresMenu)
        {
            // Do nothing
        }
        else if (programStatus == ProgramStatus.game)
        {
            // If 1 player left
            if (players.Count <= 1)
            {
                EndRound();
            }
        }
    }

    private void SetProgramStatus(ProgramStatus ps)
    {
        programStatus = ps;
        if (ps == ProgramStatus.mainMenu)
        {
            GeneralLinker.cameraManager.currentTransform = GeneralLinker.cameraManager.mainMenuTransform;
        }
        else if (ps == ProgramStatus.selectMenu)
        {
            GeneralLinker.cameraManager.currentTransform = GeneralLinker.cameraManager.selectMenuTransform;
        }
        else if (ps == ProgramStatus.scoresMenu)
        {
            scoresMenuDisplay.DisplayScores(playerScores);
            GeneralLinker.cameraManager.currentTransform = GeneralLinker.cameraManager.scoresMenuTransform;
        }
        else if (ps == ProgramStatus.game)
        {
            NextRound(true);
            GeneralLinker.cameraManager.currentTransform = GeneralLinker.cameraManager.gameTransform;
        }
    }

    public void Play()
    {
        SetProgramStatus(ProgramStatus.game);
    }

    private void EndRound()
    {
        // Cleaning
        CleanRound();

        // Choosing whether starting a new round or not
        bool endGame = false;
        foreach (int score in playerScores)
        {
            if (score >= GameConstants.endGameScore)
            {
                endGame = true;
                break;
            }
        }
        if (endGame)
        {
            SetProgramStatus(ProgramStatus.scoresMenu);
        }
        else
        {
            NextRound(false);
        }
    }

    private void CleanRound()
    {
        foreach (GameObject citizen in citizens)
        {
            Destroy(citizen);
        }
        foreach (GameObject player in players)
        {
            Destroy(player);
        }
    }
    
    private void NextRound(bool newGame)
    {
        if (newGame)
            ResetScores();

        players = new List<GameObject>();
        citizens = new List<GameObject>();

        InstantiatePlayers();
        InstantiateCitizens();

        hpEnabled = selectMenu.hpToggle.isOn;
        hpColorationEnabled = selectMenu.hpColorationToggle.isOn;
        hpRegenEnabled = selectMenu.hpRegenToggle.isOn;
    }

    private void InstantiatePlayers()
    {
        int playerId = 1;
        foreach (JoystickRegisterer registerer in selectMenu.playerRegisterers)
        {
            if (registerer.inputs.joystickId != 0)
            {
                // Creating the player
                GameObject newPlayerObject = Instantiate(playerPrefab, new Vector3(registerer.playerCoordinates.x, playerPrefab.transform.position.y, registerer.playerCoordinates.y), playerPrefab.transform.rotation);
                newPlayerObject.GetComponent<Sentinel>().gameManager = this;
                newPlayerObject.GetComponent<Player>().input = registerer.inputs;
                newPlayerObject.GetComponent<Player>().id = playerId;
                players.Add(newPlayerObject);

                // Creating the weapon
                GameObject newWeaponObject = null;
                foreach (GameObject weaponObject in weapons)
                {
                    if (weaponObject.GetComponent<Weapon>() != null && weaponObject.GetComponent<Weapon>().weaponName == registerer.weaponSelect.GetCurrentValue())
                    {
                        newWeaponObject = weaponObject;
                    }
                }
                if (newWeaponObject == null)
                    newWeaponObject = defaultWeapon;
                newWeaponObject = Instantiate(newWeaponObject, newPlayerObject.GetComponent<Sentinel>().muzleTransform);
                newWeaponObject.transform.SetParent(newPlayerObject.GetComponent<Sentinel>().turret.transform);

                // Creating the ability
                GameObject newAbilityObject = null;
                foreach (GameObject abilityObject in abilities)
                {
                    if (abilityObject.GetComponent<Ability>() != null && abilityObject.GetComponent<Ability>().abilityName == registerer.abilitySelect.GetCurrentValue())
                    {
                        newAbilityObject = abilityObject;
                    }
                }
                if (newAbilityObject == null)
                    newAbilityObject = defaultAbility;
                newAbilityObject = Instantiate(newAbilityObject, newPlayerObject.GetComponent<Sentinel>().turret.transform);
                newAbilityObject.transform.SetParent(newPlayerObject.GetComponent<Sentinel>().turret.transform);

                // Linking everything
                newPlayerObject.GetComponent<Sentinel>().weapon = newWeaponObject.GetComponent<Weapon>();
                newWeaponObject.GetComponent<Weapon>().owner = newPlayerObject.GetComponent<Sentinel>();
                newPlayerObject.GetComponent<Sentinel>().ability = newAbilityObject.GetComponent<Ability>();
                newAbilityObject.GetComponent<Ability>().owner = newPlayerObject.GetComponent<Sentinel>();

                newPlayerObject.GetComponent<Player>().SetPlayerInfoDisplayer(playerInfoDisplayers[playerId - 1]);
                
                playerId++;
            }
        }
    }

    private void InstantiateCitizens()
    {
        GameObject newCitizenObject = null;
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                newCitizenObject = Instantiate(citizenPrefab, new Vector3(i, citizenPrefab.transform.position.y, j), citizenPrefab.transform.rotation);
                newCitizenObject.GetComponent<Sentinel>().gameManager = this;
                citizens.Add(newCitizenObject);
            }
        }
    }

    private void SetScore(int playerId, int n)
    {
        playerScores[playerId - 1] = n;
        playerInfoDisplayers[playerId - 1].score.ChangeText(playerScores[playerId - 1]);
    }

    private void AddScore(int playerId, int n)
    {
        playerScores[playerId - 1] += n;
        playerInfoDisplayers[playerId - 1].score.ChangeText(playerScores[playerId - 1]);
    }

    private void ResetScores()
    {
        SetScore(1, 0);
        SetScore(2, 0);
        SetScore(3, 0);
        SetScore(4, 0);
    }

    public void DeclareDeath(GameObject victim, GameObject killer)
    {
        if (victim.GetComponent<Sentinel>().deathScored)
            return;
        victim.GetComponent<Sentinel>().deathScored = true;

        int score = GameConstants.citizenValue;

        // Removing the victim from the game
        if (victim.GetComponent<Player>() != null)
        {
            players.Remove(victim);
            score = GameConstants.playerValue;
        }
        else
        {
            citizens.Remove(victim);
            score = GameConstants.citizenValue;
        }

        // Giving points to the killer ; if suicide is committed, no point granted
        if (victim != killer)
        {
            if (killer.GetComponent<Player>() != null)
            {
                AddScore(killer.GetComponent<Player>().id, score);
            }
        }
    }
}
