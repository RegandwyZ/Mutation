 using System;
 using System.Collections.Generic;
 using UnityEngine;

 public class AllYourUnits : MonoBehaviour
 {
     public readonly List<Character> AllCharacters = new();
     
     public void AddCharacter(Character character)
     {
         AllCharacters.Add(character);
     }
     public void RemoveCharacter(Character character)
     {
         AllCharacters.Remove(character);
     }


     private void Start()
     {
         var characters = FindObjectsOfType<Character>();
         foreach (var character in characters)
         {
             if (character.GetColor == Players.Blue )
             {
                 AddCharacter(character);
             }
             
         }
     }
 }
