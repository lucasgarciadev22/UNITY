using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackCooldown;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private GameObject[] _fireballs;

    private Animator _animator;
    private PlayerMovement _playerMovement;
    private float _cooldownTimer = Mathf.Infinity; //ensure that player dont start attacking

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    /// <summary>
    ///  Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (
            Input.GetMouseButton(0)
            && _cooldownTimer > _attackCooldown
            && _playerMovement.CanAttack()
        )
        {
            AnimateAttack();
            Attack();
        }

        _cooldownTimer += Time.deltaTime;
    }

    /// <summary>
    /// Launches fireball from pooling
    /// </summary>
    private void Attack()
    {
        _fireballs[FindFireball()].transform.position = _firePoint.position;
        _fireballs[FindFireball()]
            .GetComponent<Projectile>()
            .SetDirection(Mathf.Sign(transform.localScale.x));
    }

    /// <summary>
    /// Searches in the array which objects are available (deactivated) to be using on the pooling
    /// </summary>
    /// <returns></returns>
    private int FindFireball()
    {
        for (int i = 0; i < _fireballs.Length; i++)
        {
            if (!_fireballs[i].activeInHierarchy)
                return i;
        }

        return 0;
    }

    /// <summary>
    /// Starts the attack animation
    /// </summary>
    private void AnimateAttack()
    {
        _animator.SetTrigger("attack");
        _cooldownTimer = 0;
    }
}
