using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private Material _setMaterial;
    
    private void Start()
    {
        ApplyMaterialToChildren(transform, _setMaterial);
    }

    private static void ApplyMaterialToChildren(Transform parent, Material material)
    {
        foreach (Transform child in parent)
        {
            var render = child.GetComponent<Renderer>();
            if (render != null)
            {
                render.material = material;
            }
            
            if (child.childCount > 0)
            {
                ApplyMaterialToChildren(child, material);
            }
        }
    }
}
