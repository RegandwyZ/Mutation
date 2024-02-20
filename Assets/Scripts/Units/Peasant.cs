 using System;

 public class Peasant : Character, IBuilder, IWorker
 {
     private void FixedUpdate()
     {
         UnitBehaviour();
     }


     protected override void UnitBehaviour()
     {
         if (Agent == null) return;
         if (_health <= 0)
         {
             _arrayOfUnit.RemoveCharacter(this);
             DestroyGameObject();
             AnimationPar.PlayAnimation(AnimationType.Die);
             return;
         }

         if (Agent.remainingDistance < 1.5)
         {
             Stop();
         }
         
     }

     public BasicBuilding Build()
     {
         return null;
     }

     public void ExtractResources()
     {
         
     }
 }

 