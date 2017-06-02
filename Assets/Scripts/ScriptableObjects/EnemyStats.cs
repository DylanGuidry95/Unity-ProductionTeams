using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "EnemyStat")]
public class EnemyStats : ScriptableObject
{
    public int ScoreValue;
    public int Health;
    private bool IsDead;
    public bool IsInitialized;

    public void Initialize()
    {
        IsInitialized = true;
    }

    public bool TakeDamage(int amount)
    {
        if(IsDead)
            return IsDead;
        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            IsDead = true;            
        }
        return IsDead;
    }        
}
