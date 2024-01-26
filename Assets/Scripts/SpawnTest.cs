using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnTest : MonoBehaviour
{
   private Character _unitToSpawn;
   private Vector3 _spawnPoint;
   
   private void Awake()
   {
      _unitToSpawn = FindObjectOfType<Character>();
      var position = transform.position;
      _spawnPoint = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

   }
   
   
   private void SpawnUnit(Players playerColor, Vector3 spawnPoint)
   {
      Instantiate(_unitToSpawn, spawnPoint, Quaternion.identity);
   }

   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.Space))
         SpawnUnit(Players.Blue, _spawnPoint);
   }
}
