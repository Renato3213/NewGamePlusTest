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

    public SceneData SceneData { get; set; }
    public SceneLoader SceneLoader { get; set; }
    public PlayerController Player { get; set; }
    public HealthManager Health { get; set; }
    public List<Chest> Chests { get; set; }

    private TransitionManager _transitionMananger;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SaveSystem.Save();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveSystem.Load();
        }
    }

}
