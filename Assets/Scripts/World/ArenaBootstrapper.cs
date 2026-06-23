using UnityEngine;

public class ArenaBootstrapper : MonoBehaviour
{
    [Header("Arena")]
    [SerializeField] private Vector2 arenaSize = new Vector2(28f, 28f);
    [SerializeField] private float wallHeight = 3f;
    [SerializeField] private float wallThickness = 1f;

    [Header("Materials (Optional)")]
    [SerializeField] private Material floorMaterial;
    [SerializeField] private Material wallMaterial;

    [ContextMenu("Build Arena")]
    public void BuildArena()
    {
        ClearOldArena();

        Transform root = new GameObject("ArenaRoot").transform;
        root.SetParent(transform, false);

        CreateFloor(root);
        CreateWall(root, new Vector3(0f, wallHeight * 0.5f, arenaSize.y * 0.5f), new Vector3(arenaSize.x, wallHeight, wallThickness));
        CreateWall(root, new Vector3(0f, wallHeight * 0.5f, -arenaSize.y * 0.5f), new Vector3(arenaSize.x, wallHeight, wallThickness));
        CreateWall(root, new Vector3(arenaSize.x * 0.5f, wallHeight * 0.5f, 0f), new Vector3(wallThickness, wallHeight, arenaSize.y));
        CreateWall(root, new Vector3(-arenaSize.x * 0.5f, wallHeight * 0.5f, 0f), new Vector3(wallThickness, wallHeight, arenaSize.y));
    }

    private void ClearOldArena()
    {
        Transform oldRoot = transform.Find("ArenaRoot");

        if (oldRoot != null)
        {
            if (Application.isPlaying)
            {
                Destroy(oldRoot.gameObject);
            }
            else
            {
                DestroyImmediate(oldRoot.gameObject);
            }
        }
    }

    private void CreateFloor(Transform root)
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.SetParent(root, false);
        floor.transform.localPosition = new Vector3(0f, -0.5f, 0f);
        floor.transform.localScale = new Vector3(arenaSize.x, 1f, arenaSize.y);

        if (floorMaterial != null)
        {
            floor.GetComponent<Renderer>().sharedMaterial = floorMaterial;
        }
    }

    private void CreateWall(Transform root, Vector3 localPosition, Vector3 localScale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = "Wall";
        wall.transform.SetParent(root, false);
        wall.transform.localPosition = localPosition;
        wall.transform.localScale = localScale;

        if (wallMaterial != null)
        {
            wall.GetComponent<Renderer>().sharedMaterial = wallMaterial;
        }
    }
}
