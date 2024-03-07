using System.Collections.Generic;
using UnityEngine;

public class AllYourUnits : MonoBehaviour
{
    public static AllYourUnits Instance { get; private set; }
    
    public readonly List<Character> AllCharacters = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var characters = FindObjectsOfType<Character>();
        foreach (var character in characters)
        {
            if (character.GetColor() == Players.Blue)
            {
                AddCharacter(character);
            }
        }
    }

    public void AddCharacter(Character character)
    {
        AllCharacters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        AllCharacters.Remove(character);
    }
}
