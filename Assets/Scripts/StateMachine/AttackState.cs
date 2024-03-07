using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    public AttackState(Animator animator, Character character, Collider enemy, NavMeshAgent agent)
    {
        _animator = animator;
        _character = character;
        _enemy = enemy;
        _agent = agent;
    }
    
    private readonly Animator _animator;
    private readonly Character _character;
    private Collider _enemy;
    private readonly NavMeshAgent _agent;

    public override void Enter()
    {
        AnimationParameter.Init(_animator);
    }

    public override void Update()
    {
        if (_enemy != null)
        {
            var range = CalculateEnemyPos(_enemy, _character._attackRange);

            if (_character._attackRange >= range)
            {
                AnimationParameter.PlayAnimation(AnimationType.Attack);
                _agent.isStopped = true;
                _agent.avoidancePriority = 1;
            }
            else
            {
                _character.Move(_enemy.transform.position);
                AnimationParameter.PlayAnimation(AnimationType.Run);
            }
        }
    }

    private float CalculateEnemyPos(Collider targetPos, float attackRange)
    {
        _enemy = targetPos;
        var positionEnemy = targetPos.transform.position;
        var distance = Vector3.Distance(_character.transform.position, positionEnemy);

        return distance;
    }

    public override void Exit()
    {
    }
}