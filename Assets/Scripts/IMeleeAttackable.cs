

 using UnityEngine;

 public interface IMeleeAttackable
 {
     public void MeleeAttack(Collider targetPos, int isRun, int  isAttack, float moveSpeed, float attackRange);
     
     
 }
