 using UnityEngine;

 public interface IRangeAttackable
 {
     public void RangeAttack(Collider targetPos, int isRun, int isAttack, float moveSpeed, float attackRange);
 }
