using UnityEngine;

public static class AnimationParameter
{
    private static Animator _animator;

    private static readonly int _isRun = Animator.StringToHash("IsRun");
    private static readonly int _isAttack = Animator.StringToHash("IsAttack");
    private static readonly int _isDead = Animator.StringToHash("IsDead");

    public static void Init(Animator animator)
    {
        _animator = animator;
    }

    public static void PlayAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.Idle:
                _animator.SetBool(_isRun, false);
                _animator.SetBool(_isAttack, false);
                _animator.SetBool(_isDead, false);
                break;

            case AnimationType.Run:
                _animator.SetBool(_isRun, true);
                _animator.SetBool(_isAttack, false);
                _animator.SetBool(_isDead, false);
                break;
            
            case AnimationType.Attack:
                _animator.SetBool(_isRun, false);
                _animator.SetBool(_isAttack, true);
                _animator.SetBool(_isDead, false);
                break;
            
            case AnimationType.Die:
                _animator.SetBool(_isRun, false);
                _animator.SetBool(_isAttack, false);
                _animator.SetBool(_isDead, true);
                break;
            default:
                _animator.SetBool(_isRun, false);
                _animator.SetBool(_isAttack, false);
                _animator.SetBool(_isDead, false);
                break;
        }
    }
}