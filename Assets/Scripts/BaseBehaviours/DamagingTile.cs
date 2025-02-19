using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTile : Tile
{
    // Start is called before the first frame update
    public int damage;
    int lastTurn=0;

    public DamagingTile((int x, int y) location, GameObject parent, bool walkable, bool flyable, int damage,int difficulty=1) : base(location, parent, walkable, flyable)
    {
        this.damage = damage;
        this.difficulty = difficulty;
    }

    public override void RepeatIntersect(Tile t)
    {
        Health health = t.parent.GetComponent<Health>();
        if (health && !t.flying)
        {
            health.changeHealth(-damage);
        }
    }

}

