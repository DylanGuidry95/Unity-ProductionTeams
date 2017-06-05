using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "", menuName = "EnemyStat")]
public class EnemyStats : ScriptableObject
{
    public int ScoreValue;
    public int Health;
    public int AttackPower;
    private bool IsDead;
    public bool IsInitialized;
    public EventEnemyDied OnEnemyDied;

    public void Initialize()
    {
        IsInitialized = true;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead)
            return;
        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            IsDead = true;            
            OnEnemyDied.Invoke(this);
        }
    }

    public void Attack(IDamageable target)
    {
        target.TakeDamage(AttackPower);
    }

    [SerializeField]
    public class EventEnemyDied : UnityEvent<EnemyStats>
    {
        
    }
}
