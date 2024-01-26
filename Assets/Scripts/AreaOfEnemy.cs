using UnityEngine;

 public class AreaOfEnemy: MonoBehaviour
 {
     [SerializeField] private float _rangeArea;
     [SerializeField] private LayerMask _detectionLayer;
     
     private Collider[] _result;
     
    
     
     private void Start()
     {
         _result = new Collider[25];
     }


     private void DetectEnemyInRadius()
     {
         Physics.OverlapSphereNonAlloc(transform.position, _rangeArea, _result, _detectionLayer);
     }


     private void Update()
     {
         DetectEnemyInRadius();
         FindClosestEnemy();
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
     
     
 
 
