using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class AttackRangeSearchBehavioour : MonoBehaviour
{
    public Stat AttackRange;
    public EventEnteredAttackRange OnEnterAttackRange = new EventEnteredAttackRange();
    public EventExitedAttackRange OnExitedAttackRange = new EventExitedAttackRange();

    void Start()
    {        
        var collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = AttackRange.Value;        
    }

    void OnTriggerEnter(Collider other)
    {
        OnEnterAttackRange.Invoke(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        OnExitedAttackRange.Invoke(other.gameObject);
    }

    public class EventEnteredAttackRange : UnityEvent<GameObject> { }
    public class EventExitedAttackRange : UnityEvent<GameObject> { }
}