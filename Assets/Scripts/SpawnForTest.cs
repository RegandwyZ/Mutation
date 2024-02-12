using System;
using System.Collections;
using UnityEngine;

public class SpawnForTest : MonoBehaviour
{
   [SerializeField] private Character _crossbowman;
   [SerializeField] private Character _halberdier;
   [SerializeField] private Character _knight;
   [SerializeField] private Character _soldier;
   [SerializeField] private Character _swordsman;

   [SerializeField] private Transform _spawnPos;
   private AllYourUnits _allYourUnits;

   private void Start()
   {
      _allYourUnits = FindObjectOfType<AllYourUnits>();
   }

   public void AddSwordsman()
   {
      var unit = Instantiate(_swordsman, _spawnPos.position, Quaternion.identity);
      _allYourUnits.AllCharacters.Add(unit);
   }
   
   public void AddSoldier()
   {
      var unit = Instantiate(_soldier, _spawnPos.position, Quaternion.identity);
      _allYourUnits.AllCharacters.Add(unit);
   }
   
   public void AddKnight()
   {
      var unit = Instantiate(_knight, _spawnPos.position, Quaternion.identity);
      _allYourUnits.AllCharacters.Add(unit);
   }

   
   
}