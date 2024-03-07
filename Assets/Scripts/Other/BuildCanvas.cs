using System;
using UnityEngine;

public class BuildCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _buildCanvas;
    private OrderCanvas _orderCanvas;

    private void Awake()
    {
        _orderCanvas = GetComponent<OrderCanvas>();
    }

    public void ActiveBuildCanvas()
    {
        _buildCanvas.gameObject.SetActive(true);
        _orderCanvas.DeActiveOrderCanvas();
    }

    public void DeActiveBuildCanvas()
    {
        _buildCanvas.gameObject.SetActive(false);
    }
}