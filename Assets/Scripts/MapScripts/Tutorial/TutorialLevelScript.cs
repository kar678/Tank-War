using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelScript : MonoBehaviour
{
    TutorialHUDController tutHUD;


    // Start is called before the first frame update
    void Start()
    {
        tutHUD = GameObject.FindGameObjectWithTag("TutHUD").GetComponent<TutorialHUDController>();

        StartCoroutine(StartMission());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartMission()
    {
        yield return new WaitForSeconds(1);

        if (tutHUD)
        {
            string hTitle = "Welcome Commander";
            string hBody = "I guess it has been a while since you've been on the front line? So I have arranged a series of task for you to complete. These tasks will help you outskill the enemy on the battlefield, after all we don't want to lose you. Right on your feet solider, well your in a tank so double time. Follow the dirt road for your first training mission.";
            tutHUD.ShowHint(hTitle, hBody);
        }
    }
}
