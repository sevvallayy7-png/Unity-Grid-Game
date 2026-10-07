using UnityEngine;

public class GridManager: MonoBehaviour
{  
   [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public float cellSize = 1.0f;

    [Header("Visual Elements")]
    public Transform hoverIndicator;

    private Vector2Int hoveredCell = new Vector2Int(-1, -1);

    private void Start()
    {
        if (hoverIndicator == null)
        {
            GameObject indicatorObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            indicatorObj.name = "HoverIndicator";
            Destroy(indicatorObj.GetComponent<Collider>() );
            
            indicatorObj.transform.rotation = Quaternion.Euler(90, 0, 0);

            Material yellowMat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            if (yellowMat.shader == null) yellowMat = new Material(Shader.Find("Unlit/Color"));
            yellowMat.color = Color.yellow;
            indicatorObj.GetComponent<Renderer>(). material = yellowMat;

            hoverIndicator = indicatorObj.transform;
            hoverIndicator.gameObject.SetActive(false);
          }
    }

    private void Update()
    {
       UpdateHoveredCell();
    }

    private void UpdateHoveredCell() 
    {
        Ray ray = Camera.main.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float entry))
        {  
            Vector3 worldPosition = ray. GetPoint(entry);
            int x = Mathf.FloorToInt(worldPosition.x / cellSize);
            int z = Mathf.FloorToInt(worldPosition.z / cellSize);

            if (x >= 0 && x < width && z >= 0 && z < height)
            { 
                hoveredCell = new  Vector2Int(x, z);

                if (hoverIndicator != null)
                {  
                   hoverIndicator.gameObject.SetActive(true);
                   Vector3 CellCenter = GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f;
                   hoverIndicator.position = new Vector3(CellCenter.x, 0.01f, CellCenter.z);
                }

            }
            else
            {   
                hoveredCell = new Vector2Int(-1, -1);
                if (hoverIndicator != null) hoverIndicator.gameObject.SetActive(false);
             }
         }
      }
        
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int x = 0; x < width; x++)
        {    
            for(int z = 0; z < height; z++) 
            {
             Vector3 CellCenter = GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f;
             Gizmos.DrawWireCube(CellCenter, new Vector3(cellSize, 0.01f, cellSize));
            }
         }
     }
 
    public Vector3 GetWorldPosition(int x, int z)
  { 
     return new Vector3(x, 0, z) * cellSize;
  }


} 
    
    

 

    
    



























































































