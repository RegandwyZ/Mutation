 using System.Collections.Generic;
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
         
         var buttonToPrefabMap = new Dictionary<Button, BuildingRender>
         {
             {_townHall, _prefabs._townHall},
             {_farm, _prefabs._farm},
             {_house, _prefabs._house},
             {_barrack, _prefabs._barrack},
         };

        
         foreach (var pair in buttonToPrefabMap)
         {
             pair.Key.onClick.AddListener(() => BuildBuilding(pair.Value));
         }
     }

     private void BuildBuilding(BuildingRender buildingPrefab)
     {
         _buildingsGrid.StartPlacingBuilding(buildingPrefab);
     }
 }
