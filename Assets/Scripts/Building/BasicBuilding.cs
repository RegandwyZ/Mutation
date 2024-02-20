using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicBuilding : MonoBehaviour
{
    [SerializeField] private Collider _selected;

    [SerializeField] private int _Health;
    [SerializeField] private int _armor;
    
    
    public bool IsSelected { get; private set; }
    
    public void SelectBuilding()
    {
        IsSelected = true;
        _selected.gameObject.SetActive(true);
    }
    
    public void DeSelectBuilding()
    {
        IsSelected = false;
        _selected.gameObject.SetActive(false);
    }


   
    
}
