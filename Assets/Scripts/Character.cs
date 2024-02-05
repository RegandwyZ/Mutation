using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public abstract class Character : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Animator Anim { get; set; }
    private Collider _enemyCollider;
  
    public int IsRun { get; set; }
    public int IsAttack { get; set; }
    public int IsDead { get; set; }

    [SerializeField] private RangeWeapon _bulletPrefab;
   
    [SerializeField] protected Collider _unitCollider;
    [SerializeField] protected Collider _weaponCollider;
    [SerializeField] protected Players _playersColor;
    
    [SerializeField] private Transform _bulletSpawnPos;
    
    [SerializeField] protected float _hp;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _attackRange;

    private AreaOfEnemy _areaOfEnemy;
    private bool _isSpawningBullets = false;
    private Coroutine _spawnBulletCoroutine;
    
    private void Awake()
    {
        IsRun = Animator.StringToHash("IsRun");
        IsAttack = Animator.StringToHash("IsAttack");
        IsDead = Animator.StringToHash("IsDead");

        _areaOfEnemy = GetComponent<AreaOfEnemy>();
        Physics.IgnoreCollision(_weaponCollider, _unitCollider);
        Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Move(Vector3 targetPos, int isRun, int isAttack, float moveSpeed)
    {
        if (!_agent.enabled) return;
        if (_agent == null) return;
        
        Anim.SetBool(isAttack, false);
        Anim.SetBool(isRun, true);
        _agent.speed = moveSpeed;
        _agent.SetDestination(targetPos);
        
    }
    

    public void MeleeAttack(Collider targetPos, int isRun, int isAttack, float moveSpeed, float attackRange)
    {
        _enemyCollider = targetPos;
        var range = new Vector3(-attackRange, 0, 0);
        var positionEnemy = targetPos.transform.position;
        var distance = Vector3.Distance(transform.position, positionEnemy);

        
        Move(positionEnemy - range, isRun, isAttack, moveSpeed);
        if (targetPos == null)
            Stop(isRun, isAttack);
        else
        {
            if (distance < attackRange + 0.25f)
            {
                transform.LookAt(targetPos.transform);
                _agent.isStopped = true;
                Anim.SetBool(isRun, false);
                Anim.SetBool(isAttack, true);
            }
            else if (distance > attackRange + 0.25f)
            {
                Anim.SetBool(isRun, true);
                Anim.SetBool(isAttack, false);
                _agent.enabled = true; 
                _agent.isStopped = false;
                Move(targetPos.transform.position, isRun, isAttack, moveSpeed);
            }
        }
    }
    
   

    public void RangeAttack(Collider targetPos, int isRun, int isAttack, float moveSpeed, float attackRange)
    {
        _enemyCollider = targetPos;
        var range = new Vector3(-attackRange, 0, 0);
        var positionEnemy = targetPos.transform.position;
        var distance = Vector3.Distance(transform.position, positionEnemy);
        const float speed = 2;
        
        Move(positionEnemy - range, isRun, isAttack, moveSpeed);
        if (targetPos == null)
        {
            Stop(isRun, isAttack);
            StopSpawnBulletCoroutine();
        }
        else
        {
            if (distance < attackRange + 0.25f)
            {
                if (!_isSpawningBullets)
                {
                    _spawnBulletCoroutine = StartCoroutine(SpawnBullet());
                    _isSpawningBullets = true;
                }
                
                transform.LookAt(targetPos.transform);
                _agent.isStopped = true;
                Anim.SetBool(isRun, false);
                Anim.SetBool(isAttack, true);
                
            }
            else if (distance > attackRange + 0.25f)
            {
                StopSpawnBulletCoroutine();
                Anim.SetBool(isRun, true);
                Anim.SetBool(isAttack, false);
                _agent.enabled = true; 
                _agent.isStopped = false;
                Move(targetPos.transform.position, isRun, isAttack, moveSpeed);
            }
        }
    }

    private IEnumerator SpawnBullet()
    {
        while (true)
        {
           
            var bullet = Instantiate(_bulletPrefab, _bulletSpawnPos.position, Quaternion.identity );
            bullet.Init();
            bullet.SetTarget(_enemyCollider);
            
            bullet.MoveArrow(transform);
            
            yield return new WaitForSeconds(2f);
        }
        
    }
    
    private void StopSpawnBulletCoroutine()
    {
        if (_isSpawningBullets && _spawnBulletCoroutine != null)
        {
            StopCoroutine(_spawnBulletCoroutine);
            _isSpawningBullets = false;
        }
    }

    private void Stop(int isRun, int isAttack)
    {
        _agent.isStopped = true;
        Anim.SetBool(isRun, false);
        Anim.SetBool(isAttack, false);
    }
    
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    
    protected void MeleeUpdated()
    {
        if (_agent == null) return;
        if (_hp <= 0)
        {
            Destroy(_weaponCollider );
            Destroy(_unitCollider);
            Destroy(_agent);
            Anim.SetBool(IsDead, true);
            Invoke(nameof(DestroyGameObject), 5f);
            return;
        }

        if (_agent.enabled)
        {
            if (_agent.remainingDistance < 1.5)
                Stop(IsRun, IsAttack);
        }


        if (_enemyCollider)
        {
            _enemyCollider = _areaOfEnemy.FindClosestEnemy();
            MeleeAttack(_enemyCollider, IsRun, IsAttack, _moveSpeed, _attackRange);
        }
        else
        {
            _enemyCollider = _areaOfEnemy.FindClosestEnemy();
        }

        if (_enemyCollider == null )
        {
            Stop(IsRun, IsAttack);
        }
        
       
    }
    
    protected void RangeUpdated()
    {
        if (_agent == null) return;
        if (_hp <= 0)
        {
            Destroy(_weaponCollider );
            Destroy(_unitCollider);
            Destroy(_agent);
            Anim.SetBool(IsDead, true);
            Invoke(nameof(DestroyGameObject), 5f);
            return;
        }

        if (_agent.enabled)
        {
            if (_agent.remainingDistance < 1.5)
                Stop(IsRun, IsAttack);
        }


        if (_enemyCollider)
        {
            _enemyCollider = _areaOfEnemy.FindClosestEnemy();
            RangeAttack(_enemyCollider, IsRun, IsAttack, _moveSpeed, _attackRange);
        }
        else
        {
            _enemyCollider = _areaOfEnemy.FindClosestEnemy();
        }

        if (_enemyCollider == null )
        {
            Stop(IsRun, IsAttack);
        }
    }
    
    

}
