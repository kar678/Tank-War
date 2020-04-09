using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class TaskAchievement : MonoBehaviour
{
    bool hasAchievement = false;
    public int achID;
    CoreGame core;

    // Start is called before the first frame update
    void Start()
    {
        core = GameObject.FindGameObjectWithTag("Core").GetComponent<CoreGame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player" && !hasAchievement && core)
        {
            core.UnlockAchievement(achID);
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {

        }
    }
}
