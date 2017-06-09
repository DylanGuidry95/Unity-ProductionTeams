using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "", menuName = "Attack/Attack")]
public class Attack : ScriptableObject, IDamager
{
    public Stat AttackDamage;
    public Stat AttackDelay;
    public EventAttackComplete OnAttackComplete;
    public EventAttackStarted OnAttackStarted;
    public GameObject Owner;    

    public void DoDamage(IDamageable target)
    {
        target.TakeDamage(AttackDamage.Value);
    }

    public virtual void Initialize(GameObject ownerGameObject)
    {     
        Owner = ownerGameObject;
        OnAttackStarted = new EventAttackStarted();
        OnAttackComplete = new EventAttackComplete();
    }

    public virtual void DoAttack(GameObject target)
    {
        OnAttackStarted.Invoke(Owner);
        DoDamage(target.GetComponent<IDamageable>());
    }

    [System.Serializable]
    public class EventAttackStarted : UnityEvent<GameObject>
    {
    }

    [System.Serializable]
    public class EventAttackComplete : UnityEvent<GameObject>
    {
    }
}