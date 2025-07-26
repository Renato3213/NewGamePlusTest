using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] PlayerController _pc;
    private Animator _playerAnimator;

    #region Visuals Session

    [SerializeField] SpriteRenderer _playerSpriteRenderer;

    #endregion

    void Start()
    {
        _playerAnimator= GetComponent<Animator>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerAnimator.SetBool("Grounded", _pc.Grounded);
        _playerAnimator.SetBool("Moving", _pc.MoveInput.x != 0);

        HandleSpriteFlip();
    }

    void HandleSpriteFlip()
    {
        if(_pc.MoveInput.x != 0)
        {
            _playerSpriteRenderer.flipX = _pc.MoveInput.x < 0;
        }
    }
}
