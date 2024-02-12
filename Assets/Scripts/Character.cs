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
    [SerializeField] private Collider _selected;
    
    protected NavMeshAgent Agent;
    protected Collider EnemyCollider;
    protected AnimationParameter AnimationPar;
    
    private AreaOfEnemy _areaOfEnemy;
    private Animator _animator;
    private AllYourUnits _arrayOfUnit;
    public Players GetColor => _playersColor;

    public bool IsSelected { get; private set; }
    protected bool _canAttack;
    private bool _hasStopped;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        AnimationPar = new AnimationParameter();
        AnimationPar.Init(_animator);
        _areaOfEnemy = GetComponent<AreaOfEnemy>();
        Physics.IgnoreCollision(_weaponCollider, _unitCollider);
        Agent = GetComponent<NavMeshAgent>();
        _arrayOfUnit = FindObjectOfType<AllYourUnits>();
    }
    
    public void SelectUnit()
    {
        IsSelected = true;
        _selected.gameObject.SetActive(true);
    }
    
    public void DeSelectUnit()
    {
        IsSelected = false;
        _selected.gameObject.SetActive(false);
    }
    
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    protected void UnitBehaviour()
    {
        if (Agent == null) return;
        if (_health <= 0)
        {
            _arrayOfUnit.RemoveCharacter(this);
            DestroyGameObject();
            AnimationPar.PlayAnimation(AnimationType.Die);
            return;
        }
        if (Agent.remainingDistance < 1.5)
            Stop();
        
        if (EnemyCollider && _canAttack )
        {
            Attack(EnemyCollider);
        }
        else
        {
            _areaOfEnemy.DetectEnemyInRadius();
            EnemyCollider = _areaOfEnemy.FindClosestEnemy();
            if (_canAttack)
            {
                Stop();
                _canAttack = false;
            }
           
        }

        if (EnemyCollider != null)
        {
            var range = CalculateEnemyPos(EnemyCollider, _attackRange);
            if (_attackRange >= range)
            {
                _canAttack = true;
            }
            
        }
        if (EnemyCollider == null && !_hasStopped)
        {
            Stop();
            _hasStopped = true;
            _canAttack = false;
        }
    }


    public void Move(Vector3 targetPos)
    {
        if (!Agent.enabled) return;
        if (Agent == null) return;
        Agent.isStopped = false;
        AnimationPar.PlayAnimation(AnimationType.Run);
        Agent.SetDestination(targetPos);
    }
    protected void Attack(Collider targetPos)
    {
        transform.LookAt(targetPos.transform);
        AnimationPar.PlayAnimation(AnimationType.Attack);
        Agent.isStopped = true;
        Agent.avoidancePriority = 1;
    }
    
    protected void Stop()
    {
        Agent.isStopped = true;
        AnimationPar.PlayAnimation(AnimationType.Idle);
        Agent.avoidancePriority = 50;
    }

    private float CalculateEnemyPos(Collider targetPos,  float attackRange)
    {
        EnemyCollider = targetPos;
        var positionEnemy = targetPos.transform.position;
        var distance = Vector3.Distance(transform.position, positionEnemy);

        return distance;
        
        
        // Move(positionEnemy - range);
        // if (targetPos == null)
        // {
        // AnimationPar.PlayAnimation(AnimationType.Idle);
        // Stop();
        // }
        // else
        // {
        //     if (distance < attackRange )
        //     {
        //         transform.LookAt(targetPos.transform);
        //         AnimationPar.PlayAnimation(AnimationType.Attack);
        //         Agent.isStopped = true;
        //         Agent.avoidancePriority = 1;
        //     }
        //     else if (distance > attackRange )
        //     {
        //         Agent.enabled = true; 
        //         Agent.isStopped = false;
        //         Agent.avoidancePriority = 50;
        //         AnimationPar.PlayAnimation(AnimationType.Run);
        //         Move(targetPos.transform.position);
        //     }
        // }
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