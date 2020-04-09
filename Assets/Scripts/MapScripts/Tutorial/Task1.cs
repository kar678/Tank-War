using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour
{
    TutorialHUDController tutHUD;
    TutorialObjectiveScript tutObjectiveC;
    bool shownHint = false;
    bool objectiveCompleted = false;
    bool optionalObjectiveCompleted = false;
    bool leftArea = false;
    public GameObject tank1;
    public GameObject tank2;
    public GameObject tank3;
    public GameObject nextAreaWall;

    // Start is called before the first frame update
    void Start()
    {
        tutHUD = GameObject.FindGameObjectWithTag("TutHUD").GetComponent<TutorialHUDController>();
        tutObjectiveC = GameObject.FindGameObjectWithTag("Objectives").GetComponent<TutorialObjectiveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tank1 || !tank2 || !tank3)
        {
            if (!objectiveCompleted)
            {
                tutObjectiveC.CompleteObjective2(true);
                objectiveCompleted = true;
                tutHUD.ShowHint("Well Done", "See that wasn't so hard considering they were dummy tanks. Now I am gonna get you to shoot a moving target in the next area. Just keep following the dirt road on the right");
                UnityEngine.Object.Destroy(nextAreaWall);
            }
        }

        if(!tank1 && !tank2 && !tank3)
        {
            if(!optionalObjectiveCompleted && !leftArea)
            {
                tutObjectiveC.CompleteOptionalObjective(true);
                optionalObjectiveCompleted = true;
                tutHUD.ShowHint("You destoryed them all?", "Didn't expect you to destory them all but here is an extra experience boost. Also if you complete all optional objectives you'll get something special");
                tutObjectiveC.optionalObjectivesCompleted++;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player" && !shownHint)
        {
            shownHint = true;
            tutHUD.ShowHint("Your First Task", "Right because you haven't shot a tank for a long time I need you to blow up one tank. Then we can move on to the next. Please don't get too close you need to practise your aim. By the way your weapons are now live use left click to shoot.");
            TankWeaponController twc = hitInfo.gameObject.GetComponentInChildren<TankWeaponController>();
            twc.disableControls = false;
        }

        if (hitInfo.gameObject.tag == "Player" && tutObjectiveC && !objectiveCompleted)
        {
            tutObjectiveC.ChangeObjective2(true, "Destory one of the tanks");
            tutObjectiveC.ChangeOptionalObjective(true, "Destory all of the tanks");
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            tutObjectiveC.ChangeObjective2(false, "");
            tutObjectiveC.ChangeOptionalObjective(false, "");

            if (objectiveCompleted)
            {
                leftArea = true;
                tutObjectiveC.CompleteObjective2(false);
                tutObjectiveC.CompleteOptionalObjective(false);
            }
        }
    }
}
