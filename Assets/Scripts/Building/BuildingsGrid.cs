using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
  public Vector2Int GridSize = new Vector2Int(60, 60);

    private BuildingRender[,] grid;
    private BuildingRender _flyingBuildingRender;
    private Camera mainCamera;
    
    private void Awake()
    {
        grid = new BuildingRender[GridSize.x, GridSize.y];
        
        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(BuildingRender buildingRenderPrefab)
    {
        if (_flyingBuildingRender != null)
        {
            Destroy(_flyingBuildingRender.gameObject);
        }
        
        _flyingBuildingRender = Instantiate(buildingRenderPrefab);
    }
    
    private void Update()
    {
        if (_flyingBuildingRender != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                var available = !(x < 0 || x > GridSize.x - _flyingBuildingRender.Size.x);

                if (y < 0 || y > GridSize.y - _flyingBuildingRender.Size.y) available = false;
                if (available && IsPlaceTaken(x, y)) available = false;
                if (available && IsSpaceOccupied(x, y, _flyingBuildingRender)) available = false;

                _flyingBuildingRender.transform.position = new Vector3(x, 0, y);
                _flyingBuildingRender.SetTransparent(available);
                
                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuildingRender.Size.x; x++)
        {
            for (int y = 0; y < _flyingBuildingRender.Size.y; y++)
            {
                int gridX = placeX + x;
                int gridY = placeY + y;

                if (gridX < 0 || gridX >= GridSize.x || gridY < 0 || gridY >= GridSize.y)
                {
                    return true;
                }

                if (grid[gridX, gridY] != null) return true;
            }
        }

        return false;
    }

    private bool IsSpaceOccupied(int placeX, int placeY, BuildingRender building)
    {
        var centerPoint = new Vector3(placeX + building.Size.x / 2.0f, 0, placeY + building.Size.y / 2.0f);
        var hitColliders = Physics.OverlapBox(centerPoint, 
            new Vector3(building.Size.x / 2.0f, 0.5f, building.Size.y / 2.0f), Quaternion.identity);

        if (hitColliders.Length > 2)
            return true;
        
        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuildingRender.Size.x; x++)
        {
            for (int y = 0; y < _flyingBuildingRender.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = _flyingBuildingRender;
            }
        }
        
        _flyingBuildingRender.SetNormal();
        _flyingBuildingRender = null;
    }
}
