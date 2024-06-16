using UnityEngine;
using UnityEngine.UI;

public class House : Target
{
    private ParticleSystem _particleSystem;
    [SerializeField] private Slider Slider;

    private void Awake()
    {
        if (!Visual.TryGetComponent(out _particleSystem))
        {
            Debug.LogError("No ParticleSystem component found on this GameObject", this);
        }
    }

    protected override void RaiseTargetDestroyedEvent()
    {
        base.RaiseTargetDestroyedEvent();
        Destroy(gameObject);
    }

    public override void TakeDamage(float damageTaken)
    {
        if (Health <= 0) RaiseTargetDestroyedEvent();
        Health -= damageTaken;
        Slider.value = Health / MaxHealth;
        _particleSystem.Play();
    }
}
