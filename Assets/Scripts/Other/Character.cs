using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Collider _unitCollider;
    [SerializeField] private Collider _selected;

    [SerializeField] protected Players _playersColor;

    [SerializeField] protected float _health;
    [SerializeField] public float _attackRange;

    public bool IsSelected { get; private set; }

    private NavMeshAgent _agent;
    private NavMeshObstacle _agentObstacle;
    
    protected Character _enemy;
    private StateMachine _stateMachine;

    private AreaOfEnemy _areaOfEnemy;
    private Animator _animator;
    private AllYourUnits _arrayOfUnit;

    private bool _hasStopped;
    private bool _isAttack;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = new StateMachine();
        _stateMachine.Initialize(new IdleState(_animator));
        _agent = GetComponent<NavMeshAgent>();
        _areaOfEnemy = GetComponent<AreaOfEnemy>();
        _agentObstacle = GetComponent<NavMeshObstacle>();
    }

    public Players GetColor()
    {
        return _playersColor;
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

    protected virtual void UnitBehaviour()
    {
        if (_agent == null) return;
        if (_health <= 0)
        {
            _arrayOfUnit = AllYourUnits.Instance;
            _arrayOfUnit.RemoveCharacter(this);
            DestroyGameObject();
            return;
        }

        // if (!_agent.pathPending && _agent.remainingDistance < 2)
        // {
        //     Stop();
        // }

        if (_enemy)
        {
            var distance =  CalculateEnemyPos(_enemy);
            if (distance < _attackRange&& !_isAttack)
            {
                _agent.isStopped = true;
                Attack(_enemy);
                _isAttack = true;
            }
            else if (distance > _attackRange && _isAttack )
            {
                _isAttack = false;
                _stateMachine.ChangeState(new IdleState(_animator));
            }
        }
        else
        {
            _areaOfEnemy.DetectEnemyInRadius();
            _enemy = _areaOfEnemy.FindClosestEnemy();
        }
        _enemy = _areaOfEnemy.FindClosestEnemy();
        if (_enemy == null && !_hasStopped)
        {
            _stateMachine._currentState.Exit();
            _stateMachine.ChangeState(new IdleState(_animator));
            _hasStopped = true;
        }
    }
    
    public void Move(Vector3 targetPos)
    {
        if (_agent.enabled)
        {
            _areaOfEnemy.DetectEnemyInRadius();
            _agent.avoidancePriority = 60;
            _stateMachine.ChangeState(new RunState(_animator, _agent, targetPos));
        }
       
    }

    private void Attack(Character targetPos)
    {
        transform.LookAt(targetPos.transform);
        _stateMachine.ChangeState(new AttackState(_animator, _agent, _agentObstacle, this));
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _agent.avoidancePriority = 50;
        _stateMachine.ChangeState(new IdleState(_animator));
    }

    private float CalculateEnemyPos(Character targetPos)
    {
        _enemy = targetPos;
        var positionEnemy = targetPos.transform.position;
        var distance = Vector3.Distance(transform.position, positionEnemy);
        return distance;
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