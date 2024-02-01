 using UnityEngine;

 public interface IAnimation
 { 
     public Animator Anim { get; set; }
     public  int IsRun { get; set; }
     public  int IsAttack{ get; set; }
     public  int IsDead{ get; set; }

 }
