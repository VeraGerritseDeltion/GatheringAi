using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public LayerMask unwalkableArea;
    public Vector2 gridSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt( gridSize.x / nodeDiameter);
        gridSizeY= Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottemLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottemLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableArea));
                grid[i, y] = new Node(walkable, worldPoint);
            }
        }
    }

    public Node NodeFromWP(Vector3 WorldPos)
    {
        float percentX = (WorldPos.x + gridSize.x / 2) / gridSize.x;
        float percentY = (WorldPos.z + gridSize.y / 2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));

        if ( grid != null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walk) ? Color.white : Color.red;
                Gizmos.DrawCube(n.nodePosition, Vector3.one * (nodeDiameter - .1f));
               
            }
        }

    }


}
