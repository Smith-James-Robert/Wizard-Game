using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : CharacterBase
{
    private int damage;
    private int xSpeed;
    private int ySpeed;
    public BulletBehaviour((int x, int y) location, GameObject parent, bool walkable, bool flyable,int damage, int xSpeed, int ySpeed,bool flying) : base(location, parent, walkable, flyable,flying)
    {
        this.damage = damage;
        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
    }
    public new bool Collide()
    {
        List<Tile> intersecting = GameManager.instance.grid.getValues(location.x, location.y);
        if (intersecting != null)
        {
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
                Debug.Log(intersecting[0].id);

                //ToDo Multiple intersection
                for (int i = 0; i < intersecting.Count; i++)
                {
                    Intersect(intersecting[i]);
                }
                return true;
            }
        }
        return false;
    }
    public override void Intersect(Tile t)
    {
        if (t != this)
        {
            t.RepeatIntersect(this);
            Health health = t.parent.GetComponent<Health>();
            if (health)
            {
                health.changeHealth(-damage);
                UnityEngine.Object.Destroy(parent.gameObject);
                GameManager.instance.grid.RemoveValue(location.x, location.y, this);
            }
            else if (t.flyable == false)
            {
                UnityEngine.Object.Destroy(parent.gameObject);
            }
        }
    }
    public override void RepeatIntersect(Tile t)
    {
        if (t != this)
        {
            Health health = t.parent.GetComponent<Health>();
            if (health)
            {
                health.changeHealth(-damage);
                UnityEngine.Object.Destroy(parent.gameObject);
                GameManager.instance.grid.RemoveValue(location.x, location.y, this);

            }
            else if (t.flyable == false)
            {
                UnityEngine.Object.Destroy(parent.gameObject);
                GameManager.instance.grid.RemoveValue(location.x, location.y, this);

            }
        }
    }
}
