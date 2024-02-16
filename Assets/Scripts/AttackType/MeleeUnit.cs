using UnityEngine;

public abstract class MeleeUnit : Character
{
    [SerializeField] protected Collider _weaponCollider;

    private void Start()
    {
        Physics.IgnoreCollision(_weaponCollider, _unitCollider);
    }
   
}
