 using System;
 using UnityEngine;

 
 public class OrderCanvas : MonoBehaviour
 {
     [SerializeField] private Canvas _orderCanvas;

     public void ActiveOrderCanvas()
     {
         _orderCanvas.gameObject.SetActive(true);
     }
     
     public void DeActiveOrderCanvas()
     {
         _orderCanvas.gameObject.SetActive(false);
     }
 }
