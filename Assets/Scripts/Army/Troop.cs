using UnityEngine;

public abstract class Troop : Entity
{
    [SerializeField] protected float Damage = 1;
    [SerializeField] protected float AttackSpeed = 1;
    protected Target Target;
    protected float _timer = 0f;

    public abstract void MoveTowardsPosition(Vector3 position);
    public abstract void MoveTowardsTarget(Target target);
    public abstract void Attack(Target target);
    public abstract void StopAttacking();

    private void Start()
    {
        PointTarget.PositionChanged += MoveTowardsPosition;
        PointTarget.TargetHit += MoveTowardsTarget;
        Target.TargetDestroyed += StopAttacking;
    }

    private void OnDestroy()
    {
        PointTarget.PositionChanged -= MoveTowardsPosition;
        PointTarget.TargetHit -= MoveTowardsTarget;
        Target.TargetDestroyed -= StopAttacking;
    }
}