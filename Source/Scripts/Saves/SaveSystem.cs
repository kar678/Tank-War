using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SavePlayer (Player player, int saveSlot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerSave-" + saveSlot;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer (int saveSlot)
    {
        string path = Application.persistentDataPath + "/PlayerSave-" + saveSlot;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found In" + path);
            return null;
        }
    }

    public static bool CheckFileExsits(int saveSlot)
    {
        string path = Application.persistentDataPath + "/PlayerSave-" + saveSlot;
        if(File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string GetFileLastWriteTime(int saveSlot)
    {
        string path = Application.persistentDataPath + "/PlayerSave-" + saveSlot;
        return File.GetLastWriteTime(path).ToString("HH:mm dd MMMM, yyyy");
    }

    public static void DeleteSaveFile(int saveSlot)
    {
        string path = Application.persistentDataPath + "/PlayerSave-" + saveSlot;
        File.Delete(path);
    }
}
