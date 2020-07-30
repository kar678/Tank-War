using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialObjectiveScript : MonoBehaviour
{
    public string objective1;
    public bool objective1Completed;

    public int xpForCompletingTheTutorial = 400;
    public GameObject FinishMissionUI;

    private InGameUIController gameUI;
    private CaptureZoneController captureZone;
    private PlayerStats playStats;
    private bool finishingMission;
    bool objective1C;
    bool objectiveOptionalC;
    public int optionalObjectivesCompleted = 0;
    CoreGame core;

    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameUIController>();
        playStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        if(GameObject.FindGameObjectWithTag("Core"))
        {
            core = GameObject.FindGameObjectWithTag("Core").GetComponent<CoreGame>();
        }

        if (gameUI && gameUI.objective1Text.text != objective1)
        {
            gameUI.objective1Text.text = objective1;
        }
    }

    public void ChangeObjective2(bool Show, string Objective)
    {
        gameUI.ShowObjective2(Show);
        gameUI.objective2Text.text = Objective;
    }

    public void ChangeOptionalObjective(bool Show, string Objective)
    {
        gameUI.ShowOptionalObjective(Show);
        gameUI.optionalObjectiveText.text = Objective;
    }

    public void CompleteObjective2(bool Completed)
    {
        gameUI.objective2Toggle.isOn = Completed;
    }

    public void CompleteOptionalObjective(bool Completed)
    {
        gameUI.optionalObjectiveToggle.isOn = Completed;
    }

    // Update is called once per frame
    void Update()
    {
        if (objective1Completed)
        {
            if (!objective1C)
            {
                objective1C = true;
                gameUI.objective1Toggle.isOn = true;
                playStats.objectivesCompleted = playStats.objectivesCompleted + 1;
                playStats.xpEarned = playStats.xpEarned + xpForCompletingTheTutorial;
            }

            if (!finishingMission)
            {
                gameUI.FinishMission();
            }
        }

        if (optionalObjectivesCompleted == 3)
        {
            if(!objectiveOptionalC)
            {
                objectiveOptionalC = true;
                playStats.xpEarned = playStats.xpEarned + 100;
                core.UnlockAchievement(1);
            }
            
        }
    }
}
