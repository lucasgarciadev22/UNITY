using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _jumpPower;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private LayerMask _wallLayer;

    private float _horizontalInput;
    private Rigidbody2D _body;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private const float DEFAULT_PRECISION = 0.0001f;
    private const float DEFAULT_WALLJUMP_COOLDOWN = 0.02f;
    private const float DEFAULT_GRAVITY = 2.5f;
    private float _wallJumpCoolDown;

    private bool IsJumping => Input.GetKey(KeyCode.Space) && IsGrounded();
    private bool IsWallJumping => IsOnWall() && !IsGrounded();
    private bool IsMovingLeft => _horizontalInput < -DEFAULT_PRECISION; //minimal valid variation to flip to left side
    private bool IsMovingRight => _horizontalInput > DEFAULT_PRECISION; //minimal valid variation to flip to right side
    private bool IsWalking => _horizontalInput != 0;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        _horizontalInput = GetHorizontalMovementOffset();

        AnimateMovement();
        Move();

        FlipMovementDirection();

        AnimateJump();

        if (_wallJumpCoolDown < DEFAULT_WALLJUMP_COOLDOWN)
            Jump();
        else
            _wallJumpCoolDown += Time.deltaTime;
    }

    private void FlipMovementDirection() => transform.localScale = GetHorizontalMovementDirection();

    /// <summary>
    /// Changes de player gravity scale and velocity
    /// </summary>
    private void Jump()
    {
        if (IsWallJumping)
        {
            _body.gravityScale = 0;
            _wallJumpCoolDown = 0;
            _body.velocity = new
        }
        else
            _body.gravityScale = DEFAULT_GRAVITY;

        if (IsJumping)
        {
            _body.velocity = new Vector2(_body.velocity.x, _speed);
            _animator.SetTrigger("jump");
        }
    }

    private void AnimateMovement() => _animator.SetBool("walking", IsWalking);

    private void AnimateJump() => _animator.SetBool("grounded", IsGrounded());

    private void Move() =>
        _body.velocity = new Vector2(_horizontalInput * _speed, _body.velocity.y);

    #region AUXILIARY METHODS
    /// <summary>
    /// Tries to hit the ground layer, if it detects a collision, so player is grounded
    /// </summary>
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            _boxCollider.bounds.center,
            _boxCollider.bounds.size,
            0,
            Vector2.down,
            0.01f,
            _groundLayer //layermask
        );

        return raycastHit.collider != null;
    }

    /// <summary>
    /// Tries to hit the wall layer, if it detects a collision, so player is on wall
    /// </summary>
    private bool IsOnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            _boxCollider.bounds.center,
            _boxCollider.bounds.size,
            0,
            new Vector2(transform.localScale.x, 0),
            0.01f,
            _wallLayer //layermask
        );

        return raycastHit.collider != null;
    }

    private Vector3 GetHorizontalMovementDirection()
    {
        if (IsMovingRight)
            return Vector3.one;
        else if (IsMovingLeft)
            return new Vector3(-1, 1, 1);

        return transform.localScale;
    }

    private float GetHorizontalMovementOffset()
    {
        return Input.GetAxis("Horizontal");
    }
    #endregion
}
