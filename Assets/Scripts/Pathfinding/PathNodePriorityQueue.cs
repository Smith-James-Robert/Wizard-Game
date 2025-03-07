using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PriorityQueue
{
    public List<PathNode> elements;
    public List <int> priorities;
    int size;
    public PriorityQueue()
    {
        elements = new List<PathNode>();
        priorities = new List<int>();
        size = 0;
    }
    public void MinHeapify()
    {
        if (priorities.Count > 2)
        {
            for (int i = (priorities.Count - 2) / 2; i >= 0; i--)
            {
                BubbleDown(priorities.Count, i);
            }
        }
        else
        {
            if (priorities[0] > priorities[1])
            {
                Swap(0, 1);
            }
        }
    }
    public void BubbleDown(int size,int index)
    {
        int smallest = index;
        int left = (2 * index) + 1;
        int right = (2 * index) + 2;
        if (left<size && priorities[left] < priorities[smallest])
        {
            smallest = left;
        }
        if (right<size  && priorities[right] < priorities[smallest])
        { 
            smallest = right; 
        }
        if (smallest != index)
        {
            Swap(index, smallest);
            BubbleDown(size, smallest);
        }
    }
    public void BubbleUp(int size,int index)
    {
        int parent = (index - 1) / 2;
        if (parent>=0 && priorities[index] < priorities[parent])
        {
            Swap(index, parent);
            BubbleUp(size, parent);
        }
    }
    public void Swap(int index, int destination)
    {
        (priorities[index], priorities[destination]) = (priorities[destination], priorities[index]);
        (elements[index],elements[destination]) = (elements[destination], elements[index]);
    }
    public PathNode Pop()
    {
        PathNode element = default(PathNode);
        if (priorities.Count==1)
        {
            element = elements.First();
            elements.RemoveAt(0);
            priorities.RemoveAt(0);
        }
        else if (priorities.Count==0)
        {

        }
        else
        {
            Swap(0, priorities.Count - 1);
            element = elements.Last();
            elements.RemoveAt(priorities.Count-1);
            priorities.RemoveAt(priorities.Count - 1);
            BubbleDown(priorities.Count, 0);
        }
        return element;
    }
    public void Insert (PathNode element,int priority)
    {
        elements.Add(element);
        priorities.Add(priority);
        BubbleUp(priorities.Count, priorities.Count - 1);
    }
    public PathNode GetMin()
    {
        return elements[0];
    }
    public bool IsEmpty()
    {
        return elements.Count == 0;
    }
    public bool Contains(PathNode element)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (element.location.x == elements[i].location.x  && element.location.y == elements[i].location.y)
            {
                return true;
            }
        }
        return false;
    }
    public int Length()
    {
        return (int)elements.Count;
    }

}