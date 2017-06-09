using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TargetingBehaviour : MonoBehaviour
{
    public EventTargetChanged OnTargetChanged;
    public GameObject Target;
    public LayerMask TargetCollisionLayer;
    private List<GameObject> TargetsInRange;

    [TagSelector] public string TargetTag;

    public Stat TrackingRange;

    protected virtual void Awake()
    {
        OnTargetChanged = new EventTargetChanged();
        TargetsInRange = new List<GameObject>();
        var collider = GetComponent<SphereCollider>();
        collider.radius = TrackingRange.Value;
    }

    protected virtual void Start()
    {
        SearchForTarget();
    }

    protected virtual void SearchForTarget()
    {
        if (TargetsInRange.Count < 1)
        {
            var validTargets = GameObject.FindGameObjectsWithTag(TargetTag).ToList();
            FindNearestTarget(validTargets);
            validTargets = FindNearestTarget(validTargets);
            Target = validTargets.FirstOrDefault();
            OnTargetChanged.Invoke(gameObject);
            return;
        }
        TargetsInRange = FindNearestTarget(TargetsInRange);
        Target = TargetsInRange.FirstOrDefault();
        OnTargetChanged.Invoke(gameObject);
    }

    private List<GameObject> FindNearestTarget(List<GameObject> listToSort)
    {
        var sortedList = listToSort.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));
        return sortedList.ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() == null || other.GetType() == GetType())
            return;
        TargetsInRange.Add(other.gameObject);
    }

    public class EventTargetChanged : UnityEvent<GameObject>
    {
    }
}