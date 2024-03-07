using System.Collections;
using UnityEngine;

public abstract class RangeUnit : Character
{
    [SerializeField] private RangeWeapon _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPos;
    
    private void SpawnBullet()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnPos.position, Quaternion.identity);
        bullet.Init(_playersColor);
        if (_enemyCollider == null) return;
        bullet.SetTarget(_enemyCollider);
        bullet.MoveArrow(transform);
    }

    
    // protected override void Attack(Collider targetPos,  float attackRange)
    // {
    //     if (EnemyCollider == null) return;
    //     AnimationPar.PlayAnimation(AnimationType.Run);
    //     EnemyCollider = targetPos;
    //     var range = new Vector3(-attackRange, 0, 0);
    //     var positionEnemy = targetPos.transform.position;
    //     var distance = Vector3.Distance(transform.position, positionEnemy);
    //     Move(positionEnemy - range);
    //     if (targetPos == null)
    //     {
    //         Stop();
    //         AnimationPar.PlayAnimation(AnimationType.Idle);
    //     }
    //     else
    //     {
    //         if (distance < attackRange + 0.25f)
    //         {
    //             transform.LookAt(targetPos.transform);
    //             Agent.isStopped = true;
    //             AnimationPar.PlayAnimation(AnimationType.Attack);
    //         }
    //         else if (distance > attackRange + 0.25f)
    //         {
    //             Agent.enabled = true;
    //             Agent.isStopped = false;
    //             AnimationPar.PlayAnimation(AnimationType.Run);
    //             Move(targetPos.transform.position);
    //         }
    //     }
    // }
}