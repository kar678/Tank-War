using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // Don't destroy this when loading new scenes
        DontDestroyOnLoad(gameObject);

        try
        {
            SteamClient.Init(480);
        }
        catch (System.Exception e)
        {
            // Couldn't init for some reason (steam is closed etc)
            print("Steam can't be initialized");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
