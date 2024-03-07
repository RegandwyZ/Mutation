using System.Collections;
using UnityEngine;

public class StartBuildCarcass : MonoBehaviour
{
    [SerializeField] private BuildingRender _halfProgressPrefab;
    [SerializeField] private BuildingRender _fullProgressPrefab;

    private float _progress;
    private BuildingRender _currentBuildingInstance; 

    public void StartBuild()
    {
        StartCoroutine(ConstructionProgress());
    }

    private IEnumerator ConstructionProgress()
    {
        while (_progress < 50)
        {
            _progress += Time.deltaTime * 10; 
            yield return null;
        }
        
        ReplaceBuilding(_halfProgressPrefab);
        
        while (_progress < 100)
        {
            _progress += Time.deltaTime * 10; 
            yield return null;
        }
        
        ReplaceBuilding(_fullProgressPrefab);
        Destroy(gameObject);
    }

    private void ReplaceBuilding(BuildingRender newBuildingPrefab)
    {
        if (_currentBuildingInstance != null)
        {
            Destroy(_currentBuildingInstance.gameObject); 
        }
        _currentBuildingInstance = Instantiate(newBuildingPrefab, transform.position, Quaternion.identity);
    }
}
