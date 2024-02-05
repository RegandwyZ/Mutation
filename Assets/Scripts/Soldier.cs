using UnityEngine;


public class Soldier : Character, IMeleeAttackable, IWeapon, IAnimation
{
    
    private void Update()
    {
        MeleeUpdated();
    }
    
    
    private void TakeDamage(float damage)
    {
        _hp -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        var sol = other.gameObject.GetComponent<Weapon>();

        if (sol == null || _playersColor == sol._playerColor) return;
        if (Anim.GetBool(IsAttack))
        {
            TakeDamage(10);
        }
    }
    
}