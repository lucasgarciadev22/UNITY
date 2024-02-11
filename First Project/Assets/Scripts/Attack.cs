using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackCooldown;
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private float _cooldownTimer = Mathf.Infinity; //ensure that player dont start attacking

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0) && _cooldownTimer > _attackCooldown)
            AnimateAttack();

        _cooldownTimer += Time.deltaTime;
    }

    private void AnimateAttack()
    {
        _animator.SetTrigger("attack");
        _cooldownTimer = 0;
    }
}
