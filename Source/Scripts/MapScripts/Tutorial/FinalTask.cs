using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTask : MonoBehaviour
{
    TutorialHUDController tutHUD;
    TutorialObjectiveScript tutObjectiveC;
    bool shownHint = false;
    bool objectiveCompleted = false;
    bool optionalObjectiveCompleted = false;
    bool leftArea = false;
    public GameObject tank1;
    CaptureZoneController zone;

    // Start is called before the first frame update
    void Start()
    {
        tutHUD = GameObject.FindGameObjectWithTag("TutHUD").GetComponent<TutorialHUDController>();
        tutObjectiveC = GameObject.FindGameObjectWithTag("Objectives").GetComponent<TutorialObjectiveScript>();
        zone = GameObject.FindGameObjectWithTag("Capture Zone").GetComponent<CaptureZoneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tank1 && zone.owningTeam == 1)
        {
            if (!objectiveCompleted)
            {
                tutObjectiveC.objective1Completed = true;
                tutObjectiveC.CompleteObjective2(true);
                objectiveCompleted = true;
                tutHUD.ShowHint("Well Done", "Congratulations you have completed your training I consider you ready to go on real missions, with your training done you can now fight for us. Dismissed Commander.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player" && !shownHint)
        {
            shownHint = true;
            tutHUD.ShowHint("The Real Test", "To really test I have arrenged for you to kill one of our tanks that will attack you. Then you have to capture the zone.");
            TankWeaponController twc = hitInfo.gameObject.GetComponentInChildren<TankWeaponController>();
            twc.disableControls = false;
        }

        if (hitInfo.gameObject.tag == "Player" && tutObjectiveC && !objectiveCompleted)
        {
            tutObjectiveC.ChangeObjective2(true, "Destory the enemy tank and capture zone");
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            tutObjectiveC.ChangeObjective2(false, "");

            if (objectiveCompleted)
            {
                leftArea = true;
                tutObjectiveC.CompleteObjective2(false);
            }
        }
    }
}
