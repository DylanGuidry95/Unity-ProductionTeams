using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "", menuName = "Attack/LarvaAttack")]
public class LarvaEnemyAttack : Attack
{
    public override void DoAttack(GameObject target)
    {
        base.DoAttack(target);
        Debug.Log("I'm Attacking");
        OnAttackComplete.Invoke(Owner);
    }
}
