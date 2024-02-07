using UnityEngine;

public class AreaOfEnemy : MonoBehaviour
{
    private float _rangeArea = 100;
    [SerializeField] private LayerMask _detectionLayer;

    private Collider[] _result;

    private void Awake()
    {
        _result = new Collider[10];
    }
    
    
    public void DetectEnemyInRadius()
    {
        _result = Physics.OverlapSphere(transform.position, _rangeArea, _detectionLayer);
    }


    public Collider FindClosestEnemy()
    {
        Collider closestEnemy = null;
        
        var minDistance = float.MaxValue;
        var playerPosition = transform.position;

        foreach (var enemy in _result)
        {
            if (enemy == null || enemy.gameObject == gameObject) continue;

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