using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable, IDamager
{
    [SerializeField]
    private EnemyStats _EnemyStats;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetType() == typeof(IDamageable))
            other.GetComponent<IDamageable>().TakeDamage(_EnemyStats.AttackPower);
    }

    public void TakeDamage(int amount)
    {
        _EnemyStats.TakeDamage(amount);
    }

    public void DoDamage(IDamageable target)
    {
        _EnemyStats.Attack(target);
    }
}
