 using UnityEngine;
 using UnityEngine.AI;

 public class RunState : State
 {
     
     public RunState(Animator animator, NavMeshAgent agent, Vector3 targetPos)
     {
         _animator = animator;
         _agent = agent;
         _targetPos = targetPos;
     }
     
     private readonly Animator _animator;
     private readonly NavMeshAgent _agent;
     private readonly Vector3 _targetPos;


     public override void Enter()
     {
         AnimationParameter.Init(_animator);
         AnimationParameter.PlayAnimation(AnimationType.Run);
         _agent.isStopped = false;
         _agent.SetDestination(_targetPos);
     }

     public override void Exit()
     {
         
     }

     public override void Update()
     {
         
     }
 }
