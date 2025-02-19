using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    // Start is called before the first frame update
    public (int x, int y) location;
    public int costToStart;
    public int costToEnd;
    public int trueCostToEnd;
    public bool walkable, flyable;
    public int difficulty;
    public PathNode cameFrom = null;
    public PathNode(int  x, int y, int difficulty=1,bool walkable=true, bool flyable=true)
    {
        location.x = x;
        location.y = y;
        this.walkable= walkable;
        this.flyable= flyable;
        this.difficulty = difficulty;
        costToStart = int.MaxValue;
    }
    public int CalculateExpectedDistance(int x, int y, int cost = 0)
    {
        int distance = cost;
        distance += Mathf.Abs(location.x - x);
        distance += Mathf.Abs(location.y - y);
        return distance;
    }
}
