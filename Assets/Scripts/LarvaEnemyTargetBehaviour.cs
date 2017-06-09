using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LarvaEnemyTargetBehaviour : TargetingBehaviour
{
    public NavMeshAgent Agent;

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        OnTargetChanged.AddListener(RaycastToTarget);
    }

    private void RaycastToTarget(GameObject invokedGameObject)
    {
        if (Target == null || invokedGameObject != gameObject)
            return;
        RaycastHit objectHit;
        var direction = (Target.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out objectHit, Mathf.Infinity, TargetCollisionLayer))
            if (objectHit.collider.gameObject == Target)
            {
                var hitPosition = objectHit.point;
                hitPosition.y = 0;
                Agent.destination = hitPosition;
            }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (Agent == null || Target == null)
            return;
        Gizmos.color = Color.red;
        var raise = new Vector3(0, 1, 0);
        Gizmos.DrawLine(transform.position + raise, Target.transform.position + raise);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Agent.destination + raise, 1);
    }
#endif
}