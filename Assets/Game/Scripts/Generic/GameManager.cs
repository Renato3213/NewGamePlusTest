using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //game manager is set to run before anything else
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return null;
            }

            if(instance == null)
            {
                Instantiate(Resources.Load<GameManager>("GameManager"));
            }
#endif
            return instance;
        }
    }

    public List<ItemData> allAvailableItems = new List<ItemData>();
    public List<InventorySlot> slots = new List<InventorySlot>();

    public PlayerController Player { get; set; }
    public Map Map { get; set; }
    public TransitionManager TransitionManager {get; set;}
    public HealthManager Health { get; set; }
    public List<Chest> Chests { get; set; }


    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        
    }

    public void SaveButton()
    {
        SaveSystem.Save();
    }

    public void LoadButton()
    {
        SaveSystem.Load();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void StartLoadRoutine()
    {
        StartCoroutine(SaveSystem.HandleLoadDataRoutine());
    }

}
