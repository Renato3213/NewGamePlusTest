using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform SpawnPoint => _spawnPoint;
    [SerializeField] private Transform _spawnPoint;
    void Awake()
    {
        GameManager.Instance.Map = this;
    }
    
}
