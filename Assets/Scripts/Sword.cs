 using UnityEngine;

 public class Sword: MonoBehaviour
 {
     [SerializeField] private Transform _sword;
     
     public Players _playerColor;

     public void Init(Players playerColor)
     {
         _playerColor = playerColor;
     }



 }
