using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private const int DEFAULT_LIFETIME = 5;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private bool _hit;
    private float _direction;
    private float _lifeTime;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (_hit)
            return;

        Launch();

        _lifeTime = Time.deltaTime;

        if (_lifeTime > DEFAULT_LIFETIME)
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Detects when the projectile hit something and then starts the explosion animation
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        _boxCollider.enabled = false;
        _animator.SetTrigger("explode");
    }

    /// <summary>
    /// Starts the projectile movement
    /// </summary>
    private void Launch()
    {
        float movementSpeed = _speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    /// <summary>
    /// Sets the projectile current direction
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(float direction)
    {
        gameObject.SetActive(true);

        _lifeTime = 0;
        _hit = false;
        if (_boxCollider != null)
            _boxCollider.enabled = true;
        _direction = direction;
        float localScaleX = transform.localScale.x;

        if (!IsFacingRightDirection(localScaleX))
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(
            localScaleX,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    /// <summary>
    /// Deactivates projectile after explosion and waits for next launch
    /// </summary>
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private bool IsFacingRightDirection(float localScaleX) => Mathf.Sign(localScaleX) == _direction;
}
