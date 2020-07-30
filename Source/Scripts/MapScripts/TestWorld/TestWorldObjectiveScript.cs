using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestWorldObjectiveScript : MonoBehaviour
{

    public string objective1;
    public bool objective1Completed;

    public string objective2;
    public bool objective2Completed;

    public int xpForCapturing;
    public GameObject FinishMissionUI;

    private InGameUIController gameUI;
    private CaptureZoneController captureZone;
    private PlayerStats playStats;
    private bool finishingMission;

    // Start is called before the first frame update
    void Start()
    {
        captureZone = GameObject.FindGameObjectWithTag("Capture Zone").GetComponent<CaptureZoneController>();
        gameUI = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameUIController>();
        playStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        if (gameUI)
        {
            gameUI.objective1Text.text = objective1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!objective1Completed)
        {
            if(captureZone.owningTeam == 1)
            {
                objective1Completed = true;
                gameUI.objective1Toggle.isOn = true;
                playStats.objectivesCompleted = playStats.objectivesCompleted + 1;
                playStats.xpEarned = playStats.xpEarned + xpForCapturing;
            }
        }

        if(objective1Completed)
        {
            if(!finishingMission)
            {
                StartCoroutine(FinishMission());
                Time.timeScale = 1;
            }
        }
    }

    IEnumerator FinishMission()
    {
        finishingMission = true;
        playStats.CompleteMission();
        FinishMissionUI.SetActive(true);
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        SceneManager.LoadScene("Garage");
    }
}
