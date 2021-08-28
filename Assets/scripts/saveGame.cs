using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class saveGame
{
    

    public static void performSave(playerScript player)
    {
        Debug.Log("starting save");
        Debug.Log(player.mouseSens);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.secret";
        FileStream stream = new FileStream(path, FileMode.Create);

        playerData data = new playerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static playerData loadGameData()
    {
        
        string path = Application.persistentDataPath + "/player.secret";
        Debug.Log("path: " + path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            playerData gameData = formatter.Deserialize(stream) as playerData;
            stream.Close();
            Debug.Log(gameData.playerSensSave);
            return gameData;
        }
        else
        {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }
}
