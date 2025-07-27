using UnityEngine;

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

        Door.OnDoorOpened += HandleDoor;
    }

    void HandleDoor(bool isWinCondition)
    {
        if (isWinCondition)
        {
            //win
        }

        else
        {
            //change scene
        }
    }


}
