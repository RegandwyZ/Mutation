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
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
