using UnityEngine;

public class SceneData : MonoBehaviour
{
    public SceneDataSO Data;

    private void Awake()
    {
        GameManager.Instance.SceneData = this;
    }


    public void Save(ref SceneSaveData data)
    {
        data.SceneID = Data.UniqueName;
    }

    public void Load(SceneSaveData data)
    {
        GameManager.Instance.SceneLoader.LoadSceneByIndex(data.SceneID);
    }
}
[System.Serializable]
public struct SceneSaveData
{
    public string SceneID;
}
