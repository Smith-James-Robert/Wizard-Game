using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Grid
{

    private int width, height;
    public float cellSize;
    //private TGridObject[,] gridArray;
    private List<Tile>[,] gridArray;
    private Vector3 originPosition;
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new List<Tile>[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x, y] = new List<Tile>();
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        Debug.Log("Real Width" + width + ", " + height);
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    public Vector3 GetCentralWorldPosition(int x, int y)
    {
        Vector3 central = new Vector3(x, y) * cellSize + originPosition;
        central.x += 0.5f * cellSize;
        central.y += 0.5f * cellSize;
        central.z = 0;
        return central;
    }
    private (int x, int y) GetXY(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellSize);
        int y = Mathf.FloorToInt((worldPosition.y - originPosition.y) / cellSize);
        return ((x, y));
    }
    public int GetDifficulty(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (gridArray[x, y].Count>0)
            {
                int returnValue=1;
                for (int i = 0; i< gridArray[x, y].Count; i++)
                {
                    if (gridArray[x, y][i].difficulty>returnValue)
                    {
                        returnValue = gridArray[x, y][i].difficulty; //This doesn't allow for stacking difficulty which I think is fine.
                    }
                }
                return returnValue;
            }
            else
            {
                return 1;
            }
        }
        return int.MaxValue;
    }
    public bool GetWalkable(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (gridArray[x, y].Count > 0)
            {
                for (int i = 0; i < gridArray[x, y].Count; i++)
                {
                    if (gridArray[x, y][i].walkable==false)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
    public bool GetFlyable(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (gridArray[x, y].Count > 0)
            {
                for (int i = 0; i < gridArray[x, y].Count; i++)
                {
                    if (gridArray[x, y][i].flyable == false)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public void SetValues(int x, int y, List<Tile> value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
        else
        {

        }
    }
    public void RemoveValue(int x, int y ,Tile value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y].Remove(value);
        }
    }
    public void AddValue(int x, int y, Tile value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y].Add(value);
        }
        else
        {

        }
    }
    public Tile getBaseValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y].First();
        }
        else
        {
            return default(Tile);
        }
    }
    public List<Tile> getValues(int x, int y)
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
    public void MoveTile(int x, int y, int moveX, int moveY, int id)
    {
        if ((x+moveX) < width && (y+moveY) < height && (x+moveX) >= 0 && (y+moveY) >= 0)
           {
            Tile tile = gridArray[x, y].Find(t => t.id == id);
            if (tile != null)
            {
                AddValue(x + moveX, y + moveY, tile);
                gridArray[x, y].Remove(tile);
            }
        }
    }
    public void MoveTileToLocation(int x, int y, int moveX, int moveY, int id)
    {
        if (moveX >= 0 && moveY >= 0 && moveY<height && moveX<width && x >=0 && y >=0 && x <width && y<height)
        {
            Tile tile = gridArray[x, y].Find(t => t.id == id);
            if (tile != null)
            {
                AddValue(moveX, moveY, tile);
                gridArray[x, y].Remove(tile);
            }
        }
    }
}
    //Code Source from Code Monkey but modified for purpose. https://www.youtube.com/watch?v=waEsGu--9P8