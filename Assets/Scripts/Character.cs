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
    private AreaOfEnemy _areaOfEnemy;
    private Animator _animator;
    protected AnimationParameter AnimationPar;
    
    protected abstract void Attack(Collider targetPos, float moveSpeed, float attackRange);
    
    protected void Stop() => Agent.isStopped = true;
    
    protected void DestroyGameObject() => Destroy(gameObject);
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        AnimationPar = new AnimationParameter();
        AnimationPar.Init(_animator);
        _areaOfEnemy = GetComponent<AreaOfEnemy>();
        Physics.IgnoreCollision(_weaponCollider, _unitCollider);
        Agent = GetComponent<NavMeshAgent>();
    }

    public Players GetColor => _playersColor;
    protected void UnitBehaviour()
    {
        if (Agent == null) return;
        if (_health <= 0)
        {
            Destroy(_weaponCollider);
            Destroy(_unitCollider);
            Destroy(Agent);
            AnimationPar.PlayAnimation(AnimationType.Die);
           // Invoke(nameof(DestroyGameObject), 5f);
            return;
        }
        
        if (Agent.remainingDistance < 1.5)
            Stop();
        
        if (EnemyCollider)
            Attack(EnemyCollider, _moveSpeed, _attackRange);
        else
            EnemyCollider = _areaOfEnemy.FindClosestEnemy();

        if (EnemyCollider != null) return;
        Stop();
        AnimationPar.PlayAnimation(AnimationType.Idle);
    }
    

    protected virtual void Move(Vector3 targetPos, float moveSpeed)
    {
        if (!Agent.enabled) return;
        if (Agent == null) return;
        Agent.speed = moveSpeed;
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
