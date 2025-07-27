using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneDataSO[] _sceneDataSOArray;
    private Dictionary<string, int> _sceneIDToIndexMap = new Dictionary<string,int>();

    private void Awake()
    {
        GameManager.Instance.SceneLoader = this;

        PopulateSceneMappings();
    }

    private void PopulateSceneMappings()
    {
        foreach (var sceneDataSO in _sceneDataSOArray)
        {
            _sceneIDToIndexMap[sceneDataSO.UniqueName] = sceneDataSO.SceneIndex;
        }
    }

    public void LoadSceneByIndex(string savedSceneID)
    {
        if(_sceneIDToIndexMap.TryGetValue(savedSceneID, out int sceneIndex))
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError($"No Scene found for ID: {savedSceneID}");
        }
    }
}
