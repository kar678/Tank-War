using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

public class CoreGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockAchievement(int AchID)
    {
        #if UNITY_STANDALONE
        switch (AchID)
        {
            case 1:
                var ach = new Achievement("TW_TUT_ALL_OPTIONALS");
                ach.Trigger();
                break;
            case 2:
                break;
            default:
                Debug.Log("No Achievement ID passed to function");
                break;
        }
        #endif
    }
}
