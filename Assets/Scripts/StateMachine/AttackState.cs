using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private readonly Animator _animator;
    
    private readonly NavMeshAgent _agent;
    private readonly NavMeshObstacle _obstacle;
    private readonly Character _character;


    public AttackState(Animator animator, NavMeshAgent agent, NavMeshObstacle obstacle, Character character)
    {
        _animator = animator;
        _obstacle = obstacle;
        _agent = agent;
        _character = character;
    }

    public override void Enter()
    {
        AnimationParameter.Init(_animator);
        AnimationParameter.PlayAnimation(AnimationType.Attack);
        _agent.enabled = false;
        _obstacle.enabled = true;
    }

    public override void Update()
    {
       
    }

   
    

    public override void Exit()
    {
        AnimationParameter.PlayAnimation(AnimationType.Idle);
        _character.StartCoroutine(DelayedEnableAgent());

    }
    
    private IEnumerator DelayedEnableAgent()
    {
        _obstacle.enabled = false;
        yield return new WaitForSeconds(0.01f); 
        _agent.enabled = true;
        AnimationParameter.PlayAnimation(AnimationType.Idle);
    }
}