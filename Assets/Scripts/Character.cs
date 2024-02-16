using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Collider _unitCollider;
    [SerializeField] protected Players _playersColor;
    [SerializeField] private Collider _selected;
    [SerializeField] protected float _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _attackRange;
    
    [SerializeField] private Button _stopButton;
    
    protected NavMeshAgent Agent;
    protected Collider EnemyCollider;
    protected AnimationParameter AnimationPar;
    private Canvas _canvas;
    
    private AreaOfEnemy _areaOfEnemy;
    private Animator _animator;
    protected AllYourUnits _arrayOfUnit;
    public Players GetColor => _playersColor;

    public bool IsSelected { get; private set; }
    private bool _canAttack;
    private bool _hasStopped;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        AnimationPar = new AnimationParameter();
        AnimationPar.Init(_animator);
        _areaOfEnemy = GetComponent<AreaOfEnemy>();
        _arrayOfUnit = FindObjectOfType<AllYourUnits>();
        _canvas = GetComponentInChildren<Canvas>();
        _canvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        _stopButton.onClick.AddListener(Stop);
    }

    public void SelectUnit()
    {
        IsSelected = true;
        _selected.gameObject.SetActive(true);
        _canvas.gameObject.SetActive(true);
    }
    
    public void DeSelectUnit()
    {
        IsSelected = false;
        _selected.gameObject.SetActive(false);
        _canvas.gameObject.SetActive(false);
    }
    
    protected void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    protected virtual void UnitBehaviour()
    {
        if (Agent == null) return;
        if (_health <= 0)
        {
            _arrayOfUnit.RemoveCharacter(this);
            DestroyGameObject();
            AnimationPar.PlayAnimation(AnimationType.Die);
            return;
        }

        if (Agent.remainingDistance < 1.5 )
        {
            Stop();
        }
          
        
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
        Agent.speed = _moveSpeed;
        AnimationPar.PlayAnimation(AnimationType.Run);
        Agent.SetDestination(targetPos);
    }
    public void Attack(Collider targetPos)
    {
        transform.LookAt(targetPos.transform);
        AnimationPar.PlayAnimation(AnimationType.Attack);
        Agent.isStopped = true;
        Agent.avoidancePriority = 1;
    }
    
    public void Stop()
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