using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicBuilding : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Collider _selected;

    [SerializeField] private int _Health;
    [SerializeField] private int _armor;
    
    
    public bool IsSelected { get; private set; }
    
    public void SelectBuilding()
    {
        IsSelected = true;
        ShowBuildingCanvas();
        _selected.gameObject.SetActive(true);
    }
    
    public void DeSelectBuilding()
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
