using System.Collections.Generic;
using UnityEngine;
public class Pathfinding
{
    private Grid mainGrid = GameManager.instance.grid;
    private BasicGrid pathfindingGrid;
    public (int x, int y) destination;
    private PriorityQueue path;
    public List<PathNode> paths=null;
    public Pathfinding()
    {

    }
    public Pathfinding(int x, int y)
    {
        destination.x = x;
        destination.y = y;
        path = new PriorityQueue();
    }
    private List<PathNode> GetNeighbours(int x,int y,bool flying)
    {
        List<PathNode> list=pathfindingGrid.GetNeighbours(x, y, flying);
        list.ForEach(node => { node.costToEnd = node.CalculateExpectedDistance(destination.x, destination.y);});
        return list;
    }
    public List<PathNode> GetWalkableNeighbours(int x, int y)
    {
        return GetNeighbours(x, y, false);
    }
    public List<PathNode> GetFlyableNeighbours(int x, int y)
    {
        return GetNeighbours(x, y, true);
    }
    public List<PathNode> ReconstructPath(PathNode current)
    {
        List<PathNode> path = new List<PathNode>();

        PathNode currentNode = current;
        path.Add(currentNode);
        while (currentNode.cameFrom != null) 
        {
            currentNode = currentNode.cameFrom;
            path.Insert(0,currentNode);
        }
        paths = path;
        paths.RemoveAt(0);
        return path;
       
    }
    public List<PathNode> FindPath(int x,int y, int destinationX,int destinationY, bool flying)
    {
        if ((flying && mainGrid.GetFlyable(destinationX, destinationY)) || mainGrid.GetWalkable(destinationX, destinationY) && !(destinationX == x && destinationY == y))
        {
        PriorityQueue minHeap = new PriorityQueue();
        pathfindingGrid=new BasicGrid (GameManager.WIDTH,GameManager.HEIGHT);
            destination.x = destinationX;
            destination.y=destinationY;
        PathNode currentNode = pathfindingGrid.GetValues(x, y);
            if (currentNode != null)
            {
                currentNode.costToStart = 0;
                currentNode.costToEnd=currentNode.CalculateExpectedDistance(destinationX,destinationY);
                minHeap.Insert(currentNode, 0);
                while (!minHeap.IsEmpty())
                {
                    currentNode = minHeap.Pop();
                    if (currentNode.location.x==destinationX && currentNode.location.y==destinationY)
                    {
                        return ReconstructPath(currentNode);
                    }
                    List<PathNode> neighbours = GetNeighbours(currentNode.location.x, currentNode.location.y, flying);
                    int costToStart;
                    foreach (var item in neighbours)
                    {
                        //item.costToEnd=item.CalculateExpectedDistance(destinationX, destinationY);
                        if (!flying)
                        {
                            costToStart = currentNode.costToStart + item.difficulty;
                        }
                        else
                        {
                            costToStart = currentNode.costToStart + 1;
                        }
                        
                        if (costToStart < item.costToStart)
                        {
                            item.cameFrom = currentNode;
                            item.costToStart = costToStart;
                            item.trueCostToEnd = costToStart + item.costToEnd;
                            if (!minHeap.Contains(item))
                            { 
                                minHeap.Insert(item,item.trueCostToEnd);
                            }
                        }
                    }
                }
            }
        }
        if (destinationX == x && destinationY == y)
        {
            if (paths == null)
            {
                paths = new List<PathNode>();
            }
            else
            {
                int size = paths.Count;
                for (int i = 0; i < size; i++)
                {
                    paths.RemoveAt(0);
                }
            }
        }
        else
        {
            Debug.Log("No Path to Destination!");
            paths = null;
        }
        return null;

    }


}
