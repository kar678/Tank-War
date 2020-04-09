using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerLevel;
    public float playerXP;
    public int objectivesCompleted;
    public int shotsFired;
    public int tanksKilled;

    public PlayerData (Player player)
    {
        playerLevel = player.playerLevel;
        playerXP = player.playerXP;
        objectivesCompleted = player.objectivesCompleted;
        shotsFired = player.shotsFired;
        tanksKilled = player.tanksKilled;
    }
}
