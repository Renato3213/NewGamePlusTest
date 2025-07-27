using UnityEngine;

public class FogOfDeath : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] Vector2 originalPos;

    private Rigidbody2D rb;
    private void Awake()
    {
        GameManager.Instance.Fog = this;
        rb = GetComponent<Rigidbody2D>();
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
        rb.AddForceX(speed, ForceMode2D.Force);
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
