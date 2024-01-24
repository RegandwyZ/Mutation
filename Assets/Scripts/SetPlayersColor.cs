using System;
using UnityEngine;

[Serializable]
public class SetPlayersColor : MonoBehaviour
{
   [SerializeField] private Material[] _arrMaterials;
   private Material _newMaterial;
    
   
    public void ChangeMaterialsInChildren(Transform parent, Players playersColor)
    {
        _newMaterial = playersColor switch
        {
            Players.Blue => _arrMaterials[0],
            Players.Green => _arrMaterials[1],
            _ => _newMaterial
        };

        foreach (Transform child in parent)
        {
            var component = child.GetComponent<Renderer>();

            if (component != null)
            {
               
                component.material = _newMaterial;
            }
            
            ChangeMaterialsInChildren(child, playersColor);
        }
    }
}
