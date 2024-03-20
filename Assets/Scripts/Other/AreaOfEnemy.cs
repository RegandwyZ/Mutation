using Unity.VisualScripting;
using UnityEngine;

public class AreaOfEnemy : MonoBehaviour
{
    private const float _rangeArea = 10;
    [SerializeField] private LayerMask _detectionLayer;

    private Collider[] _result;
    private Character[] _enemies;
    private void Awake()
    {
        _result = new Collider[20];
        _enemies = new Character[20];
    }
    
    
    public void DetectEnemyInRadius()
    {
        _result = Physics.OverlapSphere(transform.position, _rangeArea, _detectionLayer);
        int i = 0;
        foreach (var collider in _result)
        {
            var enemy = collider.GetComponent<Character>();
            if (enemy != null)
            {
                _enemies[i++] = enemy;
                if (i >= _enemies.Length) break; 
            }
        }
        if (i < _enemies.Length) _enemies[i] = null; 
    }

    public Character FindClosestEnemy()
    {
        Character closestEnemy = null;
        var minDistance = float.MaxValue;
        var playerPosition = transform.position;

        foreach (var enemy in _enemies)
        {
            if (enemy == null) break; 

            var enemyPosition = enemy.transform.position;
            var distance = Vector3.Distance(playerPosition, enemyPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}