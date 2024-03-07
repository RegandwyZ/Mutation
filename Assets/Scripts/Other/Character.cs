using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Collider _unitCollider;
    [SerializeField] private Collider _selected;

    [SerializeField] protected Players _playersColor;

    [SerializeField] protected float _health;
    [SerializeField] public float _attackRange;

    public bool IsSelected { get; private set; }

    private NavMeshAgent _agent;
    protected Collider _enemyCollider;
    private StateMachine _stateMachine;

    private AreaOfEnemy _areaOfEnemy;
    private Animator _animator;
    private AllYourUnits _arrayOfUnit;

    private bool _hasStopped;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = new StateMachine();
        _stateMachine.Initialize(new IdleState(_animator));
        _agent = GetComponent<NavMeshAgent>();
        _areaOfEnemy = GetComponent<AreaOfEnemy>();
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

        if (!_agent.pathPending && _agent.remainingDistance < 2)
        {
            Stop();
        }

        if (_enemyCollider)
        {
            Attack(_enemyCollider);
            _stateMachine._currentState.Update();
            _hasStopped = false;
        }
        else
        {
            _areaOfEnemy.DetectEnemyInRadius();
            _enemyCollider = _areaOfEnemy.FindClosestEnemy();
        }

        if (_enemyCollider == null && !_hasStopped)
        {
            Stop();
            _stateMachine.ChangeState(new IdleState(_animator));
            _hasStopped = true;
        }
    }
    
    public void Move(Vector3 targetPos)
    {
        _stateMachine.ChangeState(new RunState(_animator, _agent, targetPos));
    }

    private void Attack(Collider targetPos)
    {
        transform.LookAt(targetPos.transform);
        _stateMachine.ChangeState(new AttackState(_animator, this, targetPos, _agent));
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _agent.avoidancePriority = 50;
        _stateMachine.ChangeState(new IdleState(_animator));
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