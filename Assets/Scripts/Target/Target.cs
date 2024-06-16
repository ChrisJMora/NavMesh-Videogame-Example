using UnityEngine;

public abstract class Target : Entity
{
    [SerializeField] protected float Health, MaxHealth = 100;
    public abstract void TakeDamage(float damageTaken);

    public delegate void OnTargetDestroyed();
    public static event OnTargetDestroyed TargetDestroyed;

    protected virtual void RaiseTargetDestroyedEvent()
    {
        TargetDestroyed?.Invoke();
    }

    public Vector3 Position => transform.localPosition;
}