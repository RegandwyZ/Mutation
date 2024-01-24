using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Character, IAnimation, IAttackable, IWeapon
{
   
    private NavMeshAgent _agent;
    private SetPlayersColor _setPlayersColor;
    public Animator Anim { get; set; }
    
    public bool IsSelected { get; set; }

    [SerializeField] private Collider _unitCollider;
    [SerializeField] private Collider _weaponCollider; 
    [SerializeField] private float _hp; 
    [SerializeField] private float _attackRate;
    [SerializeField] private float _moveSpeed;
   
    [SerializeField] private Players _playerColor;

    private bool _isDead;
   
   private static readonly int IsRun = Animator.StringToHash("IsRun");
   private static readonly int IsAttack = Animator.StringToHash("IsAttack");
   private bool _isAttacking;
   private Character _targetPos;
   
   public void Attack(Character targetPos)
   {
      
       var range = new Vector3(-1, 0, 0);
       var position = targetPos;
       Move(position.transform.position - range);

        var distance = Vector3.Distance(transform.position, position.transform.position);
        _targetPos = position;
       
       switch (distance)
       {
           case < 2:
               _isAttacking = true;
               transform.LookAt(targetPos.transform);
               Stop();
               Anim.SetBool(IsAttack, true);
               _agent.isStopped = true;
             
               break;
           case > 2:
               _isAttacking = false;
               Anim.SetBool(IsAttack, false);
               _agent.isStopped = false;
               Move(position.transform.position);
               break;
       }
   }

   public override void Initialize(Players playerColor)
   {
       _playerColor = playerColor;
   }

   


   private void Awake()
   {
       Physics.IgnoreCollision(_weaponCollider, _unitCollider);
        _agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        _setPlayersColor = FindObjectOfType<SetPlayersColor>();
        _setPlayersColor.ChangeMaterialsInChildren(this.transform, _playerColor);
        
    }


    public override void Move(Vector3 targetPos)
    {
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
        if (_hp <= 0)
        {
            Destroy(gameObject);
            _isDead = true;
        }
            
        if(_agent.remainingDistance < 3.5)
            Stop();
        if (_targetPos != null)
        {
            Attack(_targetPos);
        }

        if (_targetPos == null)
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
       
        if (sol != null)
        {
           TakeDamage(10);
           
           
        }
       
    }
}
