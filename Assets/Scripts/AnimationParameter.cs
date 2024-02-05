﻿using UnityEngine;

public class AnimationParameter
{
    private Animator _animator;

    private readonly int _isRun = Animator.StringToHash("IsRun");
    private readonly int _isAttack = Animator.StringToHash("IsAttack");
    private readonly int _isDead = Animator.StringToHash("IsDead");

    public void Init(Animator animator)
    {
        _animator = animator;
    }

    public void PlayAnimation(AnimationType animationType)
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