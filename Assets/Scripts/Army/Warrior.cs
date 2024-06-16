using UnityEngine;
using UnityEngine.AI;

public class Warrior : Troop
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private void Awake()
    {
        if (!TryGetComponent(out _navMeshAgent))
        {
            Debug.LogError("No NavMeshAgent component found on this GameObject", this);
        }
        if (!Visual.TryGetComponent(out _animator))
        {
            Debug.LogError("No Animator component found on this GameObject", this);
        }
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
    }

    public override void MoveTowardsPosition(Vector3 position)
    {
        StopAttacking();
        _navMeshAgent.SetDestination(position);
    }

    public override void MoveTowardsTarget(Target target)
    {
        Target = target;
        _navMeshAgent.SetDestination(target.Position);
    }

    public override void Attack(Target target)
    {
        _animator.SetBool("Attack", true);
        transform.LookAt(target.Position);
        _timer += Time.deltaTime;
        if (_timer >= AttackSpeed)
        {
            target.TakeDamage(Damage);
            _timer = 0f;
        }
    }

    public override void StopAttacking()
    {
        Target = null;
        _animator.SetBool("Attack", false);
        _timer = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Target target))
        {
            if (target == Target) Attack(Target);
        }
    }
}
