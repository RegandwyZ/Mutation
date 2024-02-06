using UnityEngine;

public class SpawnForTest : MonoBehaviour
{
   [SerializeField] private Character _unit;
   [SerializeField] private Transform _spawnPoint;
   private float _value;

   private void Spawn()
   {
      Instantiate(_unit, _spawnPoint.position, Quaternion.identity);
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.S))
         Spawn();
   }
}
