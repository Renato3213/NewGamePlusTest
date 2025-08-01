using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rb => _rb;

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCol;

    #region Movement Session

    [SerializeField] float _moveSpeed;

    public Vector2 MoveInput => _moveDirection; 
    private Vector2 _moveDirection;

    #endregion


    #region Jump Session

    [SerializeField] LayerMask _groundLayer;

    bool _readyToJump = true;

    [SerializeField] float _jumpStrenght = 10f;

    [SerializeField] float _jumpCooldown = 0.1f;

    bool _jumpBuffered;
    [SerializeField] float _jumpBufferTime = 0.15f;

    [SerializeField] float _coyoteTime;
    float _coyoteTimer;

    public bool Grounded => _grounded;

    bool _grounded;

    public UnityEvent OnJump;

    #endregion

    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _boxCol = GetComponent<BoxCollider2D>();

        GameManager.Instance.Player = this;
    }

    void Update()
    {
        if (!GameManager.Instance._canPlay) return;

        _grounded = Physics2D.BoxCast(transform.position, _boxCol.size, 0, Vector2.down, 0.1f, _groundLayer);


        HandleInput();

        HandleCoyoteTime();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance._canPlay) return;

        HandleMovement();
    }

    void HandleCoyoteTime()
    {
        if (_grounded && _readyToJump)
            _coyoteTimer = _coyoteTime;
        else if (_coyoteTimer > 0)
            _coyoteTimer -= Time.deltaTime;
    }

    void HandleInput()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartJumpBuffer();
        }


        //when to jump
        if (_jumpBuffered && _readyToJump && (_grounded || _coyoteTimer > 0))
        {
            _coyoteTimer = 0;
            _jumpBuffered = false;
            _readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), _jumpCooldown);
        }
    }

    void HandleMovement()
    {
        _rb.linearVelocityX = _moveDirection.x * _moveSpeed;
    }
    void Jump()
    {
        _rb.linearVelocityY = 0;
        OnJump?.Invoke();
        _rb.AddForce(Vector2.up * _jumpStrenght, ForceMode2D.Impulse);
    }

    void ResetJump()
    {
        _readyToJump = true;
    }

    void StartJumpBuffer()
    {
        _jumpBuffered = true;
        Invoke(nameof(EndJumpBuffer), _jumpBufferTime);
    }

    void EndJumpBuffer()
    {
        _jumpBuffered = false;
    }

    public void Save(ref PlayerSaveData data)
    {
        data.Position = transform.position;
        data.Health = GameManager.Instance.Health.CurrentHealth;
    }

    public void Load(PlayerSaveData data)
    {
        transform.position = data.Position;
        GameManager.Instance.Health.CurrentHealth = data.Health;
    }

}

[System.Serializable]
public struct PlayerSaveData
{
    public Vector3 Position;
    public int Health;
}
