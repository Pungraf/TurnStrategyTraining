using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridSystemHex<TGridObject> 
{
    private const float HEX_VERTICAL_OFFSET_MULTIPLER = 0.75f;
    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridObjectArray;

    public GridSystemHex(int width, int height, float cellSize, Func<GridSystemHex<TGridObject>, GridPosition, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        
        gridObjectArray = new TGridObject[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x,z] = createGridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPostion(GridPosition gridPosition)
    {
        return 
            new Vector3(gridPosition.x, 0, 0) * cellSize +
            new Vector3(0, 0, gridPosition.z) * cellSize * HEX_VERTICAL_OFFSET_MULTIPLER + 
            (((gridPosition.z % 2) == 1) ? new Vector3(1, 0, 0) * cellSize * 0.5f : Vector3.zero);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        GridPosition roughXZ = new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize / HEX_VERTICAL_OFFSET_MULTIPLER)
            );

        bool oddRow = roughXZ.z % 2 == 1;
        List<GridPosition> neighbourGridPositionList = new List<GridPosition>
        {
            roughXZ + new GridPosition(-1, 0),
            roughXZ + new GridPosition(+1, 0),

            roughXZ + new GridPosition(0, -1),
            roughXZ + new GridPosition(0, +1),

            roughXZ + new GridPosition(oddRow ? +1 : -1, -1),
            roughXZ + new GridPosition(oddRow ? +1 : -1, +1),
        };

        GridPosition closestGridPosition = roughXZ;

        foreach(GridPosition neighbourGridPosition in neighbourGridPositionList)
        {
            if (Vector3.Distance(worldPosition, GetWorldPostion(neighbourGridPosition)) <
                Vector3.Distance(worldPosition, GetWorldPostion(closestGridPosition)))
            {
                closestGridPosition = neighbourGridPosition;
            }
        }

        return closestGridPosition;
    }

    public void CreateDebugObject(Transform debugPrefab)
    {
        for(int x = 0; x < width; x++) 
        { 
            for(int z =0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPostion(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public TGridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
               gridPosition.z >= 0 &&
               gridPosition.x < width &&
               gridPosition.z < height;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
}
