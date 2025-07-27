using UnityEngine;

public class FogOfDeath : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] Vector2 originalPos;
    private void Awake()
    {
        GameManager.Instance.Fog = this;
    }

    private void OnEnable()
    {
        TransitionManager.OnMapLoaded += ResetFog;
    }

    private void OnDisable()
    {
        TransitionManager.OnMapLoaded -= ResetFog;
    }

    void FixedUpdate()
    {
        transform.Translate(direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable;
        if (collision.TryGetComponent<Damageable>(out damageable))
        {
            damageable.TakeDamage(3, 0f);
        }
        
    }

    void ResetFog()
    {
        transform.position = originalPos;
    }

    public void Save(ref FogSaveData data)
    {
        data.Direction = direction;
        data.Position = transform.position;
    }

    public void Load(FogSaveData data)
    {
        direction = data.Direction;
        transform.position = data.Position;
    }
}

[System.Serializable]
public struct FogSaveData
{
    public Vector3 Direction;
    public Vector3 Position;

}
