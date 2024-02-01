using UnityEngine;
using UnityEngine.AI;

public class Spearman : Character, IAttackable, IWeapon, IAnimation
{
    public int IsRun { get; set; }
    public int IsAttack { get; set; }
    public int IsDead { get; set; }


    [SerializeField] private Collider _unitCollider;
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private Players _playersColor;

    [SerializeField] private float _hp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackRange;
    
    private AreaOfEnemy _areaOfEnemy;
    private Animator _anim;


    private void Awake()
    {
        IsRun = Animator.StringToHash("IsRun");
        IsAttack = Animator.StringToHash("IsAttack");
        IsDead = Animator.StringToHash("IsDead");

        _areaOfEnemy = GetComponent<AreaOfEnemy>();
        Physics.IgnoreCollision(_weaponCollider, _unitCollider);
        Anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Agent == null) return;
        if (_hp <= 0)
        {
            Destroy(_weaponCollider );
            Destroy(_unitCollider);
            Destroy(Agent);
            Anim.SetBool(IsDead, true);
            Invoke(nameof(DestroyGameObject), 5f);
            return;
        }

        if (Agent.enabled)
        {
            if (Agent.remainingDistance < 1.5)
                Stop(IsRun, IsAttack);
        }


        if (EnemyCollider)
        {
            EnemyCollider = _areaOfEnemy.FindClosestEnemy();
            Attack(EnemyCollider, IsRun, IsAttack, _moveSpeed, _attackRange);
        }
        else
        {
            EnemyCollider = _areaOfEnemy.FindClosestEnemy();
        }

        if (EnemyCollider == null )
        {
            Stop(IsRun, IsAttack);
        }
    }
    
    private void DestroyGameObject()
    {
        Destroy(gameObject);
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