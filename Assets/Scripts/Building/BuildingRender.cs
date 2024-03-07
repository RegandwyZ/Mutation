using UnityEngine;

public class BuildingRender : MonoBehaviour
{
    
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;
    private StartBuildCarcass _startBuildCarcass;

    private void Start()
    {
        _startBuildCarcass = GetComponent<StartBuildCarcass>();
    }

    public void SetTransparent(bool available)
    {
        MainRenderer.material.color = available ? Color.green : Color.red;
    }

    public void SetNormal()
    {
        MainRenderer.material.color = Color.white;
        _startBuildCarcass.StartBuild();
    }
    
    private void OnDrawGizmos()
    {
        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                Gizmos.color = (x + y) % 2 == 0 ? new Color(1f, 0.07f, 0.05f, 0.3f) : new Color(0.03f, 0.1f, 1f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
