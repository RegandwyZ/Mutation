using System.Collections;
using UnityEngine;

public class SpawnForTest : MonoBehaviour
{
    public int TestCount = 10;
    [SerializeField] private Character _unit;
    [SerializeField] private Transform _spawnPoint;
    private float _value;

    private void Spawn()
    {
        Instantiate(_unit, _spawnPoint.position, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(SpawnCO());
    }

    private IEnumerator SpawnCO()
    {
        for (var i = 0; i < 15; i++)
        {
            Spawn();
            yield return new WaitForSeconds(0.1f);
        }
    }
}