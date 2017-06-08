public class LarvaEnemyAttackBehaviour : AttackBehaviour
{
    public Stat AttackRange;
    public LarvaEnemyTargetBehaviour _LarvaTargetBehaviour;

    void Start()
    {
                
    }

    private bool IsInAttackRange()
    {
        if (_LarvaTargetBehaviour.Target == null)
            return false;
        return true;
    }

    protected override void Update()
    {
        if (!IsInAttackRange()) return;
        base.Update();
    }
}