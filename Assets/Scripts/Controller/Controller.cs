// using UnityEngine;
//
// public class Controller : MonoBehaviour
// {
//     private Soldier _soldier;
//     private AreaOfEnemy _arrOfEnemy;
//     private void Update()
//     {
//        // var arrayEnemy = _arrOfEnemy.DetectEnemyInRadius();
//
//
//         if (Camera.main == null) return;
//         var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//         if (Physics.Raycast(ray, out var hit))
//         {
//             
//             var hitSoldier = hit.collider.GetComponent<Soldier>();
//             
//             if (Input.GetMouseButtonDown(0))
//             {
//                 if (hitSoldier != null)
//                 {
//                     _soldier = hitSoldier;
//                     _soldier.IsSelected = !_soldier.IsSelected;
//                    
//                 }
//             }
//
//             if (Input.GetMouseButtonDown(1) && _soldier != null && _soldier.IsSelected)
//             {
//                 var hitCollider = hit.collider.GetComponent<Soldier>();
//                 _soldier.Move(hit.point);
//                 if (hitCollider)
//                 {
//                     _soldier.Attack(hitCollider);
//                    
//                 }
//                     
//             }
//         }
//     }
// }
