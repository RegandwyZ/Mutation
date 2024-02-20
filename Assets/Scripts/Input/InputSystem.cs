using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InputSystem : MonoBehaviour
{
    [SerializeField] private AllYourUnits _arrayOfAllYourUnits;
    [SerializeField] private Texture _boxSelect;
    [SerializeField] private Button _stop;
    [SerializeField] private Button _build;
    
    private OrderCanvas _orderCanvas;
    private BuildCanvas _buildCanvas;
    private readonly List<Character> _selectedCharacters = new();
    
    private BasicBuilding _selectedBuilding;
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private Rect s_rect;
    private Camera _mainCamera;
    private bool _selected;
    private bool _findUnit;
    private float _width;
    private float _height;


    private void Awake()
    {
        _mainCamera = Camera.main;
        _stop.onClick.AddListener(StopSelectedUnits);
       
        _orderCanvas = GetComponent<OrderCanvas>();
        _buildCanvas = GetComponent<BuildCanvas>();
        
        _build.onClick.AddListener(_buildCanvas.ActiveBuildCanvas);
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(Input.GetMouseButtonDown(0))
            HandleLeftClick();
        
        if (_selected)
            SelectingRect();
        
        else if (_findUnit)
            AddSelectedUnitInArray();
        
        if (Input.GetMouseButtonDown(1))
            HandleRightClick();
        
    }

    private void SelectingRect()
    {
        _endPoint = Input.mousePosition;
        if(Input.GetMouseButtonUp(0))
        {
            s_rect = SelectRect(_startPoint, _endPoint);
            _findUnit = true;
            _selected = false;
        }
    }

    private void AddSelectedUnitInArray()
    {
        foreach(var tmp in _arrayOfAllYourUnits.AllCharacters)
        {
            var pos = _mainCamera.WorldToScreenPoint(tmp.transform.position);
            pos.y = InvertY(pos.y);
            if (s_rect.Contains(pos))
            {
                _selectedCharacters.Add(tmp);
                tmp.SelectUnit();
                _orderCanvas.ActiveOrderCanvas();
            }
        }
        _findUnit = false;
    }

    private void HandleLeftClick()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            var character = hit.collider.GetComponent<Character>();
            if (character != null )
            {
                if (_selectedBuilding != null)
                {
                    BuildingDeselect();
                }
                _selectedCharacters.Add(character);
                character.SelectUnit();
                _orderCanvas.ActiveOrderCanvas();
                return;
            }
                
            var building = hit.collider.GetComponent<BasicBuilding>();
            if (building != null)
            {
                if (building == _selectedBuilding)
                {
                    BuildingDeselect();
                    return;
                }
                _selectedBuilding = building;
                building.SelectBuilding();
                return;
            }
        }
            
        _startPoint = Input.mousePosition;
        foreach (var character in _selectedCharacters)
        {
            if (character != null)
            {
                character.DeSelectUnit();
                _orderCanvas.DeActiveOrderCanvas();
                _buildCanvas.DeActiveBuildCanvas();
            }
        }
        _selectedCharacters.Clear();
        _selected = true;
    }
    private void HandleRightClick()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            foreach (var character in _selectedCharacters)
            {
                if (character.IsSelected && character != null)
                {
                    character.Move(hit.point);
                }
            }
        }
    }

    private void OnGUI()
    {
        if(_selected)
        {
            _width = _endPoint.x - _startPoint.x;
            _height = InvertY(_endPoint.y) - InvertY(_startPoint.y);
            GUI.DrawTexture(new Rect(_startPoint.x, InvertY(_startPoint.y), _width, _height), _boxSelect);
        }
    }

    private Rect SelectRect(Vector3 _start, Vector3 _end)
    {
        if (_width < 0.0f)
        {
            _width = Mathf.Abs(_width);
        }
        if (_height < 0.0f)
        {
            _height = Mathf.Abs(_height);
        }

        if (_endPoint.x < _startPoint.x)
        {
            _start.z = _start.x;
            _start.x = _end.x;
            _end.x = _start.z;
        }
        if (_endPoint.y > _startPoint.y)
        {
            _start.z = _start.y;
            _start.y = _end.y;
            _end.y = _start.z;
        }

        return new Rect(_start.x, InvertY(_start.y), _width, _height);
    }

    private void BuildingDeselect()
    {
        _selectedBuilding.DeSelectBuilding();
        _selectedBuilding = null;
    }
    private float InvertY(float _y)
    {
        return Screen.height - _y;
    }
    
    public void StopSelectedUnits()
    {
        foreach (var character in _selectedCharacters)
        {
            if (character != null)
            {
                character.Stop();
            }
        }
    }
}