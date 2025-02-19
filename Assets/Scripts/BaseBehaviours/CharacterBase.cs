using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : Tile
{
    private bool _blocking;
    public CharacterBase((int x, int y) location, GameObject parent, bool walkable, bool flyable, bool flying = false) : base (location,parent,walkable,flyable,flying)
        {
    }
    public new bool isBase
    {
        get { return false; }
    }
    public Tile GetBaseNeighbour(int i)
    {
        switch (i)
        {
            case 0:
                return GameManager.instance.grid.getBaseValue(location.x + 1, location.y);
            case 1:
                return GameManager.instance.grid.getBaseValue(location.x - 1, location.y);
            case 2:
                return GameManager.instance.grid.getBaseValue(location.x, location.y+1);
            case 3:
                return GameManager.instance.grid.getBaseValue(location.x, location.y-1);
            default:
                return null;
        }
    }
    public bool IsWalkable(List<Tile> tiles)
    {
        for (int i=0; i<tiles.Count; i++)
        {
            if (!tiles[i].movable)
            {
                return false;
            }
        }
        return true;
    }
    public bool IsFlyable(List<Tile> tiles)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (!tiles[i].flyable)
            {
                return false;
            }
        }
        return true;
    }

}
