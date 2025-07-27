using UnityEngine;

public class FogOfDeath : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;

    private void Awake()
    {
        GameManager.Instance.Fog = this;
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
