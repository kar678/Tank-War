using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int shotsFired;
    public int objectivesCompleted;
    public float xpEarned;
    public int tanksKilled;

    public void CompleteMission()
    {
        PlayerPrefs.SetInt("MissionJustCompleted", 1);
        PlayerPrefs.SetInt("ShotsFired", shotsFired);
        PlayerPrefs.SetInt("ObjectivesCompleted", objectivesCompleted);
        PlayerPrefs.SetFloat("XPEarned", xpEarned);
        PlayerPrefs.SetInt("TanksKilled", tanksKilled);
    }
}
