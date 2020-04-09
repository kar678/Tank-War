using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2 : MonoBehaviour
{
    TutorialHUDController tutHUD;
    TutorialObjectiveScript tutObjectiveC;
    bool shownHint = false;
    bool objectiveCompleted = false;
    bool optionalObjectiveCompleted = false;
    bool leftArea = false;
    public GameObject tank;
    public GameObject tank2;
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
        if (!tank | !tank2)
        {
            if (!objectiveCompleted)
            {
                tutObjectiveC.CompleteObjective2(true);
                objectiveCompleted = true;
                tutHUD.ShowHint("Well Done", "Now that you know how to shoot a moving target I am now gonna get you to fight a tank thats going to shoot you. Just follow the dirt road into the next area.");
                UnityEngine.Object.Destroy(nextAreaWall);
            }
        }

        if(!tank && !tank2)
        {
            if(!optionalObjectiveCompleted && !leftArea)
            {
                optionalObjectiveCompleted = true;
                tutObjectiveC.CompleteOptionalObjective(true);
                tutObjectiveC.optionalObjectivesCompleted++;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player" && !shownHint)
        {
            shownHint = true;
            tutHUD.ShowHint("Your Second Task", "Here I am getting you to destory the moving tank to practise your leading skills with the weapon on the tank. Beware that tank shells don't go to far. This tank needs to be hit 3 times to be destoryed.");
            TankWeaponController twc = hitInfo.gameObject.GetComponentInChildren<TankWeaponController>();
            twc.disableControls = false;
        }

        if (hitInfo.gameObject.tag == "Player" && tutObjectiveC && !objectiveCompleted)
        {
            tutObjectiveC.ChangeObjective2(true, "Destory the moving tank");
            tutObjectiveC.ChangeOptionalObjective(true, "Destory all of the moving tanks");
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
