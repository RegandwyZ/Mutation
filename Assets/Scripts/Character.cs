using UnityEngine;
using UnityEngine.AI;



public abstract class Character : MonoBehaviour
{
    protected NavMeshAgent Agent;
    public Animator Anim { get; set; }
    protected Collider EnemyCollider;
  
    
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Move(Vector3 targetPos, int isRun, int isAttack, float moveSpeed)
    {
        if (!Agent.enabled) return;
        if (Agent == null) return;
        
        Anim.SetBool(isAttack, false);
        Anim.SetBool(isRun, true);
        Agent.speed = moveSpeed;
        Agent.SetDestination(targetPos);

    }
    

    public void Attack(Collider targetPos, int isRun, int isAttack, float moveSpeed, float attackRange)
    {
        EnemyCollider = targetPos;
        var range = new Vector3(-attackRange, 0, 0);
        var positionEnemy = targetPos.transform.position;
        var distance = Vector3.Distance(transform.position, positionEnemy);

        
        Move(positionEnemy - range, isRun, isAttack, moveSpeed);
        if (targetPos == null)
            Stop(isRun, isAttack);
        else
        {
            if (distance < attackRange + 0.5f)
            {
                transform.LookAt(targetPos.transform);
                Agent.isStopped = true;
                Anim.SetBool(isRun, false);
                Anim.SetBool(isAttack, true);
            }
            else if (distance > attackRange + 0.5f)
            {
                Anim.SetBool(isRun, true);
                Anim.SetBool(isAttack, false);
                Agent.enabled = true; 
                Agent.isStopped = false;
                Move(targetPos.transform.position, isRun, isAttack, moveSpeed);
            }
        }
    }
    
    protected  void Stop(int isRun, int isAttack)
    {
        Agent.isStopped = true;
        Anim.SetBool(isRun, false);
        Anim.SetBool(isAttack, false);
      //  Agent.isStopped = false;
        
    }
    
   
    
    

}
