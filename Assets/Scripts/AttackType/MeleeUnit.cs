using UnityEngine;

public abstract class MeleeUnit : Character
{

    protected override void Attack(Collider targetPos, float moveSpeed, float attackRange)
    {
        AnimationPar.PlayAnimation(AnimationType.Run);
        EnemyCollider = targetPos;
        var range = new Vector3(-attackRange, 0, 0);
        var positionEnemy = targetPos.transform.position;
        var distance = Vector3.Distance(transform.position, positionEnemy);
       
        Move(positionEnemy - range, moveSpeed);
        if (targetPos == null)
        {
            AnimationPar.PlayAnimation(AnimationType.Idle);
            Stop();
        }
        else
        {
            if (distance < attackRange )
            {
                transform.LookAt(targetPos.transform);
                AnimationPar.PlayAnimation(AnimationType.Attack);
                Agent.isStopped = true;
            }
            else if (distance > attackRange )
            {
                Agent.enabled = true; 
                Agent.isStopped = false;
                AnimationPar.PlayAnimation(AnimationType.Run);
                Move(targetPos.transform.position, moveSpeed);
            }
        }
    }
}
