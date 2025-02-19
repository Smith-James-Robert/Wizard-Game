using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BasicGrid
{

    private int width, height;
    public float cellSize;
    //private TGridObject[,] gridArray;
    private PathNode[,] gridArray;
    private Vector3 originPosition;
    private Grid grid = GameManager.instance.grid;
    public BasicGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
        gridArray = new PathNode[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x, y] = new PathNode(x, y, grid.GetDifficulty(x, y), grid.GetWalkable(x, y), grid.GetFlyable(x, y));
            }
        }
    }
    public int GetDifficulty(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            int returnValue = 1;
            returnValue = gridArray[x, y].difficulty;
            return returnValue;
        }
        return int.MaxValue;
    }
    public bool GetWalkable(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return (gridArray[x, y].walkable);
        }
        return false;
    }
    public bool GetFlyable(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return (gridArray[x, y].walkable);
        }
        return false;
    }
    public List<PathNode> GetNeighbours(int x, int y, bool flying)
    {
        List<PathNode> neighbours = new List<PathNode>();
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (x>0)
            {
                if (flying && GetFlyable(x-1,y))
                {
                    neighbours.Add(GetValues(x - 1, y));
                }
                else if (GetWalkable(x-1,y))
                {
                    neighbours.Add(GetValues(x-1, y));
                }
            }
            if (x<width-1)
            {
                if (flying && GetFlyable(x + 1, y))
                {
                    neighbours.Add(GetValues(x + 1, y));
                }
                else if (GetWalkable(x + 1, y))
                {
                    neighbours.Add(GetValues(x + 1, y));
                }
            }
            if (y>0)
            {
                if (flying && GetFlyable(x, y - 1))
                {
                    neighbours.Add(GetValues(x, y - 1));
                }
                else if (GetWalkable(x, y - 1))
                {
                    neighbours.Add(GetValues(x, y - 1));
                }
            }
            if (y<height-1)
            {
                if (flying && GetFlyable(x, y + 1))
                {
                    neighbours.Add(GetValues(x, y + 1));
                }
                else if (GetWalkable(x, y + 1))
                {
                    neighbours.Add(GetValues(x, y + 1));
                }
            }
        }
            return neighbours;
    }
    public PathNode GetValues(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return null;
        }
    }
}

