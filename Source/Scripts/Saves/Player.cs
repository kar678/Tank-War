using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int playerLevel = 1;
    public float playerXP = 0;
    public float xpNeededForNextLevel;
    public float xpAddedEachLevel = 150;
    public int shotsFired = 0;
    public int tanksKilled = 0;
    public int objectivesCompleted = 0;
    public TextMeshProUGUI playerLevelText;
    public Slider xpSlider;
    private bool LevelingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("SaveSlot", 0) > 0)
        {
            if (SaveSystem.CheckFileExsits(PlayerPrefs.GetInt("SaveSlot")))
            {
                PlayerData data = SaveSystem.LoadPlayer(PlayerPrefs.GetInt("SaveSlot"));

                playerLevel = data.playerLevel;
                playerXP = data.playerXP;
            }
            else
            {
                SaveSystem.SavePlayer(this, PlayerPrefs.GetInt("SaveSlot"));
            }
            xpNeededForNextLevel = xpAddedEachLevel * playerLevel;

            if (PlayerPrefs.GetInt("MissionJustCompleted", 0) == 1)
            {
                shotsFired = shotsFired + PlayerPrefs.GetInt("ShotsFired", 0);
                objectivesCompleted = objectivesCompleted + PlayerPrefs.GetInt("ObjectivesCompleted", 0);
                playerXP = playerXP + PlayerPrefs.GetFloat("XPEarned", 0);
                tanksKilled = tanksKilled + PlayerPrefs.GetInt("TanksKilled", 0);
                SaveSystem.SavePlayer(this, PlayerPrefs.GetInt("SaveSlot"));
                PlayerPrefs.SetInt("MissionJustCompleted", 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(playerXP > xpNeededForNextLevel && !LevelingUp)
        {
            LevelingUp = true;
            LevelUp();
        }

        if(playerLevelText)
        {
            playerLevelText.text = "Level: " + playerLevel;
        }

        if(xpSlider)
        {
            xpSlider.value = playerXP / xpNeededForNextLevel;
        }
    }

    void LevelUp()
    {
        playerXP = playerXP - xpNeededForNextLevel;
        playerLevel = playerLevel + 1;
        LevelingUp = false;
    }

    public void SaveUserData()
    {
        if(PlayerPrefs.GetInt("SaveSlot", 0) > 0)
        {
            SaveSystem.SavePlayer(this, PlayerPrefs.GetInt("SaveSlot"));
        }
    }
}
