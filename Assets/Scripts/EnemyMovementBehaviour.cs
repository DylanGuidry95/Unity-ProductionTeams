using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementBehaviour : MonoBehaviour, IDamager
{
    private NavMeshAgent _NavMeshAgent;
    [SerializeField]
    public Stats EnemyStats;
    [SerializeField]
    private Transform TargetTower;
    public float MovementSpeed;
    public float DistanceFromTarget;
    public bool CanWalk;
    [Tooltip("Distance the enemy must be from target to trigger a state change")]
    public float DistanceToTrigger;
   
    private enum States
    {
        idle, walk, attack
    }

    [SerializeField]
    private States CurrentState;

    public EventEnemyLarvaAttack OnEnemyLarvaAttack;

    void Start()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
        _NavMeshAgent.stoppingDistance = DistanceToTrigger;
        SearchForTarget();
        _NavMeshAgent.destination = TargetTower.position;        
        _NavMeshAgent.speed = MovementSpeed;
        StartCoroutine("Idle");
    }

    void OnDrawGizmos()
    {
        if(_NavMeshAgent)
            Gizmos.DrawLine(transform.position, _NavMeshAgent.destination);
    }

    IEnumerator Idle()
    {
        int LoopCounter = 0;
        CurrentState = States.idle;
        _NavMeshAgent.isStopped = true;                        
        while (LoopCounter <= 1000)
        {
            LoopCounter++;
            DistanceFromTarget = Vector3.Distance(transform.position, TargetTower.position);
            transform.LookAt(TargetTower.position);
            if (DistanceFromTarget > DistanceToTrigger)
            {                
                yield return StartCoroutine("Walk");
            }
            else if(CurrentState != States.attack)
                CurrentState = States.attack;
            yield return null;
        }        
    }

    public float attackTimer;
    public bool canAttack = true;

    private void Update()
    {
        if (TargetTower == null)
        {
            SearchForTarget();
        }

        if (TargetTower.GetComponent<IDamageable>() == null || CurrentState != States.attack)
            return;

        if(canAttack)
            attackTimer += Time.deltaTime;

        if (attackTimer >= EnemyStats.Items["larvaenemyattackspeed"].Value)
        {
            canAttack = false;
            OnEnemyLarvaAttack.Invoke(this.gameObject);
        }
    }

    void SearchForTarget()
    {
        var validTargets = GameObject.FindGameObjectsWithTag("PlayerTower").ToList();
        if (validTargets == null || validTargets.Count == 0)
        {
            TargetTower = null;
            _NavMeshAgent.destination = this.transform.position;
            return;
        }
        validTargets = validTargets.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToList();
        TargetTower = validTargets[0].transform;
    }


    IEnumerator Walk()
    {
        int LoopCounter = 0;
        CurrentState = States.walk;
        _NavMeshAgent.isStopped = false;
        while (LoopCounter <= 1000)
        {
            LoopCounter++;
            DistanceFromTarget = Vector3.Distance(transform.position, TargetTower.position);
            if (DistanceFromTarget <= DistanceToTrigger)
                yield return StartCoroutine("Idle");
            yield return null;
        }
    }

    public void ResetAttackTimer()
    {
        attackTimer = 0;
        canAttack = false;
    }

    public void StartAttackTimer()
    {
        canAttack = true;
    }

    public void DoDamage()
    {
        TargetTower.GetComponent<IDamageable>().TakeDamage(10);
    }

    [System.Serializable]
    public class EventEnemyLarvaAttack : UnityEvent<GameObject>
    {
        
    }

    public void DoDamage(IDamageable target)
    {
        target.TakeDamage(10);
    }
}
