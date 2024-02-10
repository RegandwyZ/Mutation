using UnityEngine;
using UnityEngine.AI;


public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Collider _unitCollider;
    [SerializeField] protected Collider _weaponCollider;
    [SerializeField] protected Players _playersColor;
    [SerializeField] protected float _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _attackRange;
    
    protected NavMeshAgent Agent;
    protected Collider EnemyCollider;
    protected AnimationParameter AnimationPar;
    
    private AreaOfEnemy _areaOfEnemy;
    private Animator _animator;
    public Players GetColor => _playersColor;

    public bool IsSelected { get; private set; }
    
    protected abstract void Attack(Collider targetPos, float attackRange);

    public void SelectUnit()
    {
        IsSelected = true;
    }
    
    public void DeSelectUnit()
    {
        IsSelected = false;
    }
    
    protected void Stop()
    {
        Agent.isStopped = true;
        AnimationPar.PlayAnimation(AnimationType.Idle);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        AnimationPar = new AnimationParameter();
        AnimationPar.Init(_animator);
        _areaOfEnemy = GetComponent<AreaOfEnemy>();
        Physics.IgnoreCollision(_weaponCollider, _unitCollider);
        Agent = GetComponent<NavMeshAgent>();
    }

    protected void UnitBehaviour()
    {
        if (Agent == null) return;
        if (_health <= 0)
        {
            DestroyGameObject();
            AnimationPar.PlayAnimation(AnimationType.Die);
            return;
        }

        if (Agent.remainingDistance < 1.5)
            Stop();
        if (EnemyCollider)
            Attack(EnemyCollider, _attackRange);
        else
        {
            _areaOfEnemy.DetectEnemyInRadius();
            EnemyCollider = _areaOfEnemy.FindClosestEnemy();
        }

        if (EnemyCollider != null) return;
        Stop();
        Agent.avoidancePriority = 50;



    }


    public virtual void Move(Vector3 targetPos)
    {
        if (!Agent.enabled) return;
        if (Agent == null) return;
        Agent.isStopped = false;
        AnimationPar.PlayAnimation(AnimationType.Run);
        Agent.SetDestination(targetPos);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        var sol = other.gameObject.GetComponent<Weapon>();
        if (sol == null || _playersColor == sol._playerColor) return;
        TakeDamage(10);
    }
}