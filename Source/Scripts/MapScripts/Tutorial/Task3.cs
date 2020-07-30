using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3 : MonoBehaviour
{
    TutorialHUDController tutHUD;
    TutorialObjectiveScript tutObjectiveC;
    bool shownHint = false;
    bool objectiveCompleted = false;
    bool optionalObjectiveCompleted = false;
    bool leftArea = false;
    public GameObject tank;
    public GameObject nextAreaWall;
    public int bulletsDodged;

    // Start is called before the first frame update
    void Start()
    {
        tutHUD = GameObject.FindGameObjectWithTag("TutHUD").GetComponent<TutorialHUDController>();
        tutObjectiveC = GameObject.FindGameObjectWithTag("Objectives").GetComponent<TutorialObjectiveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tank)
        {
            if (!objectiveCompleted)
            {
                tutObjectiveC.CompleteObjective2(true);
                objectiveCompleted = true;
                tutHUD.ShowHint("Well Done", "Now that you know how to fight an enemy tank you'll will now fight another tank then capture the objective in the next area. Just follow the dirt road into the next area.");
                UnityEngine.Object.Destroy(nextAreaWall);
            }
        }

        if (bulletsDodged >= 3)
        {
            if (!optionalObjectiveCompleted && !leftArea)
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
            tutHUD.ShowHint("Your Third Task", "Here I am getting you to destory the enemy tank to practise your fighting skills with the tank you have. This tank needs to be hit 5 times to be destoryed.");
            TankWeaponController twc = hitInfo.gameObject.GetComponentInChildren<TankWeaponController>();
            twc.disableControls = false;
        }

        if (hitInfo.gameObject.tag == "Player" && tutObjectiveC && !objectiveCompleted)
        {
            tutObjectiveC.ChangeObjective2(true, "Destory the tank before it kills you");
            tutObjectiveC.ChangeOptionalObjective(true, "Dodge Three shots");
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
