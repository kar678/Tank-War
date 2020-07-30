using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class InGameUIController : MonoBehaviour
{
    private TankController playerTank;
    public GameObject pauseMenu;
    public TextMeshProUGUI reloadText;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public bool drawHealth;
    private bool isPauseMenuVisible = false;
    public TextMeshProUGUI objective1Text;
    public Toggle objective1Toggle;
    public bool hasObjective2;
    public TextMeshProUGUI objective2Text;
    public Toggle objective2Toggle;
    public bool hasObjective3;
    public TextMeshProUGUI objective3Text;
    public Toggle objective3Toggle;
    public bool hasOptionalObjective;
    public TextMeshProUGUI optionalObjectiveText;
    public Toggle optionalObjectiveToggle;
    public bool tutorialUI = false;
    public AudioMixer masterMixer;
    bool finishingMission = false;
    private PlayerStats playStats;

    public GameObject FinishMissionUI;
    public GameObject ReturnButton;
    public TextMeshProUGUI ShotsFiredText;
    public TextMeshProUGUI ObjectivesCompletedText;
    public TextMeshProUGUI TanksDestoryedText;
    public TextMeshProUGUI XPEarnedText;
    bool UpdateValues = false;
    float Shots;
    float Objectives;
    float Tanks;
    float XP;

    //Player Vars

    private float maxHealth = 0;
    private float currentHealth = 0;

    void Start()
    {
        playerTank = GameObject.FindGameObjectWithTag("Player").GetComponent<TankController>();
        playStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        maxHealth = playerTank.maxHitPoints;

        if(!hasObjective2)
        {
            objective2Text.gameObject.SetActive(false);
            objective2Toggle.gameObject.SetActive(false);
        }

        if(!hasObjective3)
        {
            objective3Text.gameObject.SetActive(false);
            objective3Toggle.gameObject.SetActive(false);
        }

        if(!hasOptionalObjective)
        {
            optionalObjectiveText.gameObject.SetActive(false);
            optionalObjectiveToggle.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))  
        {
            if (isPauseMenuVisible)
            {
                isPauseMenuVisible = false;
                pauseMenu.SetActive(false);
                masterMixer.SetFloat("GameVol", 0);

                if (!tutorialUI)
                {
                    Time.timeScale = 1;
                }
            }
            else if (!isPauseMenuVisible)
            {
                isPauseMenuVisible = true;
                pauseMenu.SetActive(true);
                masterMixer.SetFloat("GameVol", -80);

                if (!tutorialUI)
                {
                    Time.timeScale = 0;
                }
            }
        }

        currentHealth = playerTank.currentHitPoints;

        if(drawHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            healthText.text = string.Format("{0}/{1}", (currentHealth).ToString("0"), maxHealth);
        }
        else
        {
            healthText.gameObject.SetActive(false);
            healthSlider.gameObject.SetActive(false);
        }

        if(UpdateValues)
        {
            float STimes = playStats.shotsFired / 4;
            float OTimes = playStats.objectivesCompleted / 4;
            float TTimes = playStats.tanksKilled / 4;
            float XPTimes = playStats.xpEarned / 4;

            Shots = (Shots < playStats.shotsFired) ? Shots + Time.deltaTime * STimes : playStats.shotsFired;
            Objectives = (Objectives < playStats.objectivesCompleted) ? Objectives + Time.deltaTime * OTimes : playStats.objectivesCompleted;
            Tanks = (Tanks < playStats.tanksKilled) ? Tanks + Time.deltaTime * TTimes : playStats.tanksKilled;
            XP = (XP < playStats.xpEarned) ? XP + Time.deltaTime * XPTimes : playStats.xpEarned;

            ShotsFiredText.text = (Shots).ToString("0");
            ObjectivesCompletedText.text = (Objectives).ToString("0");
            TanksDestoryedText.text = (Tanks).ToString("0");
            XPEarnedText.text = (XP).ToString("0");
        }
    }

    public void Resume()
    {
        isPauseMenuVisible = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void SetReloading(bool reloading)
    {
        if (reloading)
        {
            reloadText.gameObject.SetActive(true);
        }
        else if (!reloading)
        {
            reloadText.gameObject.SetActive(false);
        }
    }

    public void ShowObjective2(bool Show)
    {
        objective2Text.gameObject.SetActive(Show);
        objective2Toggle.gameObject.SetActive(Show);
    }

    public void ShowObjective3(bool Show)
    {
        objective3Text.gameObject.SetActive(Show);
        objective3Toggle.gameObject.SetActive(Show);
    }

    public void ShowOptionalObjective(bool Show)
    {
        optionalObjectiveText.gameObject.SetActive(Show);
        optionalObjectiveToggle.gameObject.SetActive(Show);
    }

    public void FinishMission()
    {
        if(!finishingMission)
        {
            StartCoroutine(FinishingMission());
        }
    }

    IEnumerator FinishingMission()
    {
        finishingMission = true;
        playStats.CompleteMission();
        FinishMissionUI.SetActive(true);
        UpdateValues = true;
        yield return new WaitForSeconds(5);
        UpdateValues = false;
        Time.timeScale = 1;
        ReturnButton.SetActive(true);
    }
}
