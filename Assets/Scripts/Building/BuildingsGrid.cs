using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize;

    private BuildingRender[,] grid;
    private BuildingRender _flyingBuildingRender;
    private Camera mainCamera;
    private Character _worker;

    private Vector3 _targetBuildingPosition;
    private bool _isPlacementConfirmed;

    private void Awake()
    {
        grid = new BuildingRender[GridSize.x, GridSize.y];
        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(BuildingRender buildingRenderPrefab)
    {
        _isPlacementConfirmed = false;
        if (_flyingBuildingRender != null)
        {
            Destroy(_flyingBuildingRender.gameObject);
        }

        _flyingBuildingRender = Instantiate(buildingRenderPrefab);
    }

    private void Update()
    {
        if (_flyingBuildingRender == null) return;
        if (!_isPlacementConfirmed)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float position))
            {
                var worldPosition = ray.GetPoint(position);
                _targetBuildingPosition =
                    new Vector3(Mathf.RoundToInt(worldPosition.x), 0, Mathf.RoundToInt(worldPosition.z));
                _flyingBuildingRender.transform.position = _targetBuildingPosition;
            }
        }

        var x = Mathf.RoundToInt(_targetBuildingPosition.x);
        var y = Mathf.RoundToInt(_targetBuildingPosition.z);

        var available = !(x < 0 || x > GridSize.x - _flyingBuildingRender.Size.x) &&
                        !(y < 0 || y > GridSize.y - _flyingBuildingRender.Size.y);
        available &= !IsPlaceTaken(x, y) && !IsSpaceOccupied(x, y, _flyingBuildingRender);

        _flyingBuildingRender.SetTransparent(available);

        if (available && Input.GetMouseButtonDown(0) && !_isPlacementConfirmed)
        {
            PlaceFlyingBuilding(x, y);
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (var x = 0; x < _flyingBuildingRender.Size.x; x++)
        {
            for (var y = 0; y < _flyingBuildingRender.Size.y; y++)
            {
                var gridX = placeX + x;
                var gridY = placeY + y;

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
        return hitColliders.Length > 2;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        _targetBuildingPosition = new Vector3(placeX, 0, placeY);

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