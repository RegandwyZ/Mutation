using UnityEngine;
using UnityEngine.AI;

public class Soldier : Character, IAnimation, IAttackable, IWeapon
{
    private NavMeshAgent _agent;
   
    public Animator Anim { get; set; }

    public bool IsSelected { get; set; }

    [SerializeField] private Collider _unitCollider;
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private float _hp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Players _playersColor;
    private AreaOfEnemy _areaOfEnemy;
    private Collider _enemyCollider;

    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    
    

    private void Awake()
    {
        _areaOfEnemy = GetComponent<AreaOfEnemy>();
        Physics.IgnoreCollision(_weaponCollider, _unitCollider);
        _agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    public void Attack(Collider targetPos)
    {
        _enemyCollider = targetPos;
        var range = new Vector3(-1, 0, 0);
        var positionEnemy = targetPos.transform.position;
        var distance = Vector3.Distance(transform.position, positionEnemy);

        
        Move(positionEnemy - range);
        if (targetPos == null)
            Stop();
        
        switch (distance)
        {
            case < 2:
                transform.LookAt(targetPos.transform);
                _agent.isStopped = true;
                Anim.SetBool(IsRun, false);
                Anim.SetBool(IsAttack, true);
                break;
        
            case > 2:
                Anim.SetBool(IsRun, true);
                Anim.SetBool(IsAttack, false);
                _agent.enabled = true; 
                _agent.isStopped = false;
                Move(targetPos.transform.position);
                break;
        }
    }

    
    public override void Move(Vector3 targetPos)
    {
        if (!_agent.enabled) return;
        if (_agent == null) return;
        
        Anim.SetBool(IsAttack, false);
        Anim.SetBool(IsRun, true);
        _agent.speed = _moveSpeed;
        _agent.SetDestination(targetPos);

    }

    protected override void Stop()
    {
        _agent.isStopped = true;
        Anim.SetBool(IsRun, false);
        Anim.SetBool(IsAttack, false);
        _agent.isStopped = false;
        
    }
    

    private void Update()
    {
        if (_agent == null) return;
        if (_hp <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (_agent.enabled)
        {
            if (_agent.remainingDistance < 1.5)
                Stop();
        }
       

        if (_enemyCollider)
        {
            _enemyCollider = _areaOfEnemy.FindClosestEnemy();
            Attack(_enemyCollider);
        }
        else
        {
            _enemyCollider = _areaOfEnemy.FindClosestEnemy();
        }

        if (_enemyCollider == null)
        {
            Stop();
        }
        
       
    }
    
    
    

    private void TakeDamage(float damage)
    {
        _hp -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        var sol = other.gameObject.GetComponent<Sword>();

        if (sol == null || _playersColor == sol._playerColor) return;
        if (Anim.GetBool(IsAttack))
        {
            TakeDamage(10);
        }
    }
    
   
    
}