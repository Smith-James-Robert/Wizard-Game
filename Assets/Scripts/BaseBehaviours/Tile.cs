using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private bool _walkable;
    private bool _flyable;
    private bool _flying;
    private int _difficulty;
    public (int x, int y) location;
    private GameObject _parent;
    public int id;
    private static int countingId = 0;
    public Tile((int x, int y) location,GameObject parent, bool walkable = true, bool flyable = true, bool flying = false, int difficulty = 1)
    {
        this._walkable = walkable;
        this._flyable = flyable;
        this.location = location;
        this._parent = parent;
        _flying = flying;
        id = countingId;
        countingId++;
        _difficulty = difficulty;
    }
    public int difficulty
    {
        get { return _difficulty; }
        set { _difficulty = value; }
    }
    public GameObject parent
    {
        get { return _parent; }
    }
    public bool walkable
    {
        get { return _walkable; }
        set { _walkable = value;
            if (_walkable)
                flyable = walkable;
            }
    }
    public bool flyable
    {
        get { return _flyable; }
        set { _flyable = value; }
    }
    public bool movable
    {
        get
        {
            return (_walkable && _flyable);
        }
    }
    public bool isBase
    {
        get { return true; }
    }
    public int health
    {
        get { return -1; }
    }
    public bool flying
    {
        get { return _flying; }
        set { _flying = value; }
    }

    public List<Tile> Collisions()
    {
        return GameManager.instance.grid.getValues(location.x, location.y);
    }
    public bool Collide()
    {
        List<Tile> intersecting = GameManager.instance.grid.getValues(location.x, location.y);
        if (intersecting != null)
        {
            if (intersecting.Count > 0)
            {

            }
            if (intersecting.Count == 0)
            {
                return false;
            }
            else if (intersecting.Count == 1)
            {
                Intersect(intersecting[0]);
                return true;
            }
            else
            {
                for (int i = 0; i < intersecting.Count; i++)
                {
                    Intersect(intersecting[i]);
                }
                return true;
            }
        }
        return false;
    }
    public virtual void Intersect(Tile t)
    {
        if (t != this)
        {
            t.RepeatIntersect(this);
        }
    }
    public virtual void RepeatIntersect(Tile t)
    {
    }
}
