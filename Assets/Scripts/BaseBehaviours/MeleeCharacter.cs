using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacter : CharacterBase
{
    private bool _blocking;
    public int damage;
    public MeleeCharacter((int x, int y) location, GameObject parent, bool walkable, bool flyable,int damage, bool flying = false) : base(location, parent, walkable, flyable, flying)
    {
        this.damage = damage;
    }
    public new bool isBase
    {
        get { return false; }
    }
    public bool Collide(Tile t)
    {
        return false;
        
    }
    public void Attack(Tile t)
    { 
            Health health = t.parent.GetComponent<Health>();
            if (health)
            {
                health.changeHealth(-damage);
            }
    }

}
