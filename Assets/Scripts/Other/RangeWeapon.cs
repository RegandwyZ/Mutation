using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Collider _target;
    private Players _color;

    public void Init(Players playerColor)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _color = playerColor;
    }

    public void SetTarget(Collider newTarget)
    {
        _target = newTarget;
        transform.LookAt(_target.transform);
    }

    public void MoveArrow(Transform orig)
    {
        if (_target == null || orig == null)
        {
            DestroyArrow();
            return;
        }

        Invoke(nameof(DestroyArrow), 5f);
        var direction = (_target.transform.position - orig.position).normalized;
        _rigidbody.AddForce(direction * _speed, ForceMode.Impulse);
    }

    private void DestroyArrow() => Destroy(gameObject);

    private void OnTriggerEnter(Collider other)
    {
        var sol = other.gameObject.GetComponent<Character>();
        if (sol == null || _color == sol.GetColor()) return;
        sol.TakeDamage(10);
        DestroyArrow();
    }
    
    
}