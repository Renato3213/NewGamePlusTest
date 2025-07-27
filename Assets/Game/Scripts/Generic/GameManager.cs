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

    public PlayerController Player { get; set; }
    public FogOfDeath Fog { get; set; }
    public Map Map { get; set; }
    public TransitionManager TransitionManager {get; set;}
    public HealthManager Health { get; set; }
    public List<InventorySlot> InventorySlots { get { return _inventorySlots; } set { _inventorySlots = value; } }
    [SerializeField] private List<InventorySlot> _inventorySlots;

    [SerializeField] GameObject _winScreen;
    [SerializeField] GameObject _loseScreen;

    public bool _canPlay = true;

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

    public void Win()
    {
        _winScreen.SetActive(true);
        _canPlay = false;
    }

    public void Lose()
    {
        _loseScreen.SetActive(true);
        _canPlay = false;
    }

    public void ReplayButton()
    {
        _canPlay = true;
        SceneManager.LoadScene("Game");
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
