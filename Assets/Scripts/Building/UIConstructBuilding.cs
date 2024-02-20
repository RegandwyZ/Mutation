 using System;
 using UnityEngine;
 using UnityEngine.UI;

 public class UIConstructBuilding : MonoBehaviour
 {
     [SerializeField] private Button _townHall;
     [SerializeField] private Button _farm;
     [SerializeField] private Button _house;
     [SerializeField] private Button _barrack;
     [SerializeField] private Button _blackSmith;
     [SerializeField] private Button _stables;
     [SerializeField] private Button _archery;

     [SerializeField] private BuildingPrefabs _prefabs;
     private BuildingsGrid _buildingsGrid;
     private void Start()
     {
         _buildingsGrid = FindObjectOfType<BuildingsGrid>();
         
         _townHall.onClick.AddListener(BuildTownHall);
         _farm.onClick.AddListener(BuildFarm);
         _house.onClick.AddListener(BuildHouse);
         _barrack.onClick.AddListener(BuildBarrack);
     }

     private void BuildTownHall()
     {
         _buildingsGrid.StartPlacingBuilding(_prefabs._townHall);
     }
     private void BuildFarm()
     {
         _buildingsGrid.StartPlacingBuilding(_prefabs._farm);
     }
     private void BuildHouse()
     {
         _buildingsGrid.StartPlacingBuilding(_prefabs._house);
     }
     private void BuildBarrack()
     {
         _buildingsGrid.StartPlacingBuilding(_prefabs._barrack);
     }
 }
