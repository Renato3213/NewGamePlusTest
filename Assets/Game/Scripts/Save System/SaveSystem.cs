using UnityEngine;
using System.IO;
using System.Collections;
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
        public List<InventorySlotSaveData> SlotSaveData;
        public MapSaveData MapSaveData;
        public FogSaveData FogSaveData;
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
        GameManager.Instance.TransitionManager.Save(ref _saveData.MapSaveData);
        GameManager.Instance.Fog.Save(ref _saveData.FogSaveData);

        _saveData.ChestsData = new List<ChestSaveData>();
        foreach (Chest chest in GameObject.FindObjectsByType<Chest>(FindObjectsSortMode.None))
        {
            ChestSaveData data = new ChestSaveData();
            chest.Save(ref data);
            _saveData.ChestsData.Add(data);
        }

        _saveData.SlotSaveData = new List<InventorySlotSaveData>();
        foreach (InventorySlot slot in GameObject.FindObjectsByType<InventorySlot>(FindObjectsSortMode.None))
        {
            InventorySlotSaveData data = new InventorySlotSaveData();
            slot.Save(ref data);
            _saveData.SlotSaveData.Add(data);
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
        //GameManager.Instance.Player.Load(_saveData.PlayerData);
        //GameManager.Instance.TransitionManager.Load(_saveData.MapSaveData);

        //foreach (var chestData in _saveData.ChestsData)
        //{
        //    Chest chest = GameObject.FindObjectsByType<Chest>(FindObjectsSortMode.None)
        //                      .FirstOrDefault(c => c.ChestUID == chestData.ChestUID);
        //    chest?.Load(chestData);
        //}

        //foreach (var slotData in _saveData.SlotSaveData)
        //{
        //    InventorySlot slot = GameObject.FindObjectsByType<InventorySlot>(FindObjectsSortMode.None)
        //                      .FirstOrDefault(c => c.SlotUID == slotData.slotUID);
        //    slot?.Load(slotData);
        //}
        GameManager.Instance.StartLoadRoutine();
        
    }

    public static IEnumerator HandleLoadDataRoutine()//very poor solution for a problem
    {
        GameManager.Instance.TransitionManager.Load(_saveData.MapSaveData);

        yield return new WaitForSeconds(0.75f);

        GameManager.Instance.Player.Load(_saveData.PlayerData);
        GameManager.Instance.Fog.Load(_saveData.FogSaveData);
        foreach (var chestData in _saveData.ChestsData)
        {
            Chest chest = GameObject.FindObjectsByType<Chest>(FindObjectsSortMode.None)
                              .FirstOrDefault(c => c.ChestUID == chestData.ChestUID);
            chest?.Load(chestData);
        }

        foreach (var slotData in _saveData.SlotSaveData)
        {
            InventorySlot slot = GameObject.FindObjectsByType<InventorySlot>(FindObjectsSortMode.None)
                              .FirstOrDefault(c => c.SlotUID == slotData.slotUID);
            slot?.Load(slotData);
        }
    }
}
