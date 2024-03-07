using UnityEngine;

public class IdleState : State
{
    public IdleState(Animator animator)
    {
        _animator = animator;
    }

    private readonly Animator _animator;

    public override void Enter()
    {
        AnimationParameter.Init(_animator);
        AnimationParameter.PlayAnimation(AnimationType.Idle);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}