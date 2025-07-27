using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class SaveSystem
{
    private static SaveData _saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public PlayerSaveData PlayerData;
        public List<ChestSaveData> ChestsData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {
        GameManager.Instance.Player.Save(ref _saveData.PlayerData);

        _saveData.ChestsData = new List<ChestSaveData>();
        foreach (Chest chest in GameObject.FindObjectsByType<Chest>(FindObjectsSortMode.None))
        {
            ChestSaveData data = new ChestSaveData();
            chest.Save(ref data);
            _saveData.ChestsData.Add(data);
        }
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        GameManager.Instance.Player.Load(_saveData.PlayerData);

        foreach (var chestData in _saveData.ChestsData)
        {
            Chest chest = GameObject.FindObjectsByType<Chest>(FindObjectsSortMode.None)
                              .FirstOrDefault(c => c.ChestUID == chestData.ChestUID);
            chest?.Load(chestData);
        }
    }
}
