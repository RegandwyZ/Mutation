using System;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
     [SerializeField] private float _speed;
     
     private Rigidbody _rigidbody;
     private Collider _target;

     
     public void Init()
     {
          _rigidbody = GetComponent<Rigidbody>();
     }

     public void SetTarget(Collider newTarget)
     {
          _target = newTarget;
          transform.LookAt(_target.transform);
     }

     public void MoveArrow(Transform orig)
     {
          
          if (_target == null || orig == null )
          {
               Destroy(gameObject);
               return;
          }
          var direction = (_target.transform.position - orig.position).normalized ;
          _rigidbody.AddForce(direction * _speed,  ForceMode.Impulse );
          
     }
    
     

}
