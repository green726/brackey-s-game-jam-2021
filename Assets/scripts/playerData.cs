using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class playerData
{
    public string playerLevelString;
    public int playerLevelInt;
    public float playerSensSave;
    public string[] unlockedPowerUps;

    public playerData(playerScript player) {
        playerLevelString = player.playerLevelString;
        playerLevelInt = player.playerLevelInt;
        playerSensSave = player.mouseSens;
    }

 
}
