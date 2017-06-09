using UnityEngine;

[RequireComponent(typeof(TargetingBehaviour))]
public class AttackBehaviour : MonoBehaviour
{
    private TargetingBehaviour _TargetingBehaviour;
    public AttackScriptable AttackConfig;
    public bool InAttackRange;

    void Awake()
    {
        var AttackRangeBehaviour = GetComponentInChildren<AttackRangeSearchBehavioour>();
        AttackRangeBehaviour.OnEnterAttackRange.AddListener(IsTargetInRange);
        AttackRangeBehaviour.OnExitedAttackRange.AddListener(IsTargetInRange);
    }

    void Start()
    {
        AttackConfig = Instantiate(AttackConfig);
        AttackConfig.Initialize(gameObject);
        _TargetingBehaviour = GetComponent<TargetingBehaviour>();
    }

    [SerializeField] private float AttackTimer;

    void IsTargetInRange(GameObject other)
    {
        if (other == _TargetingBehaviour.Target)
            InAttackRange = !InAttackRange;
    }

    protected virtual void Update()
    {
        if(_TargetingBehaviour.Target == null)
            return;
        if (AttackTimer >= AttackConfig.AttackDelay.Value && InAttackRange)
        {
            AttackConfig.DoAttack(_TargetingBehaviour.Target);
            AttackTimer = 0;
        }
        AttackTimer += Time.deltaTime;
    }
}