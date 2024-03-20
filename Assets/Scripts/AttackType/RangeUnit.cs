using System.Collections;
using UnityEngine;

public abstract class RangeUnit : Character
{
    [SerializeField] private Transform _bulletSpawnPos;
    
    private void SpawnBullet()
    {
        var bullet = ArrowPool.SharedInstance.GetArrow().GetComponent<RangeWeapon>();
        bullet.Init(_playersColor);
        bullet.transform.position = _bulletSpawnPos.position;
        bullet.transform.rotation = _bulletSpawnPos.rotation;
        
        if (_enemy != null)
        {
            bullet.SetTarget(_enemy);
            bullet.MoveArrow(transform);
        }
    }
    
}