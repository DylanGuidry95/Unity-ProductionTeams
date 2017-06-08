using UnityEngine;

[RequireComponent(typeof(TargetingBehaviour))]
public class AttackBehaviour : MonoBehaviour, IDamager
{
    private TargetingBehaviour _TargetingBehaviour;
    public Stat AttackDamage;
    public Stat AttackDelay;

    [SerializeField] private float AttackTimer;

    public void DoDamage(IDamageable target)
    {
        target.TakeDamage(AttackDamage.Value);
    }

    protected virtual void Update()
    {
        if (AttackTimer >= AttackDelay.Value)
            DoDamage(_TargetingBehaviour.Target.GetComponent<IDamageable>());
        AttackTimer += Time.deltaTime;
    }
}