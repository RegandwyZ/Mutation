using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuilding : MonoBehaviour
{

    [SerializeField] private Character _unit;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Collider _selected;
    public bool IsSelected { get; private set; }
    
    public void SelectUnit()
    {
        IsSelected = true;
        ShowBuildingCanvas();
        _selected.gameObject.SetActive(true);
    }
    
    public void DeSelectUnit()
    {
        IsSelected = false;
        HideBuildingCanvas();
        _selected.gameObject.SetActive(false);
    }


    private void ShowBuildingCanvas()
    {
        _canvas.SetActive(true);
    }
    private void HideBuildingCanvas()
    {
        _canvas.SetActive(false);
    }
    
}
