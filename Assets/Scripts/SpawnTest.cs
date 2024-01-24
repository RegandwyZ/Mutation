using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
   private Character _unitToSpawn;
   private Vector3 _spawnPointGreen;
   private Vector3 _spawnPointBlue;
   private void Awake()
   {
      _unitToSpawn = FindObjectOfType<Character>();
      var position = transform.position;
      _spawnPointGreen = new Vector3(position.x, position.y, position.z);
      _spawnPointBlue = new Vector3(position.x + 5, position.y, position.z);

   }

   private void Start()
   {
      SpawnUnit(Players.Blue, _spawnPointBlue);
      SpawnUnit(Players.Green, _spawnPointGreen);
   }


   private void SpawnUnit(Players playerColor, Vector3 spawnPoint)
   {
      _unitToSpawn.Initialize(playerColor);
      Instantiate(_unitToSpawn, spawnPoint, Quaternion.identity);
   }
}
