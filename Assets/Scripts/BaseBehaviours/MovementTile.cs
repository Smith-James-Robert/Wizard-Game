using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovementTile : Tile
{
    // Start is called before the first frame update

    public int moveX, moveY, linkedX, linkedY;
    public bool actionAfterMove;
    public bool teleport;
    public MovementTile((int x, int y) location, GameObject parent, bool walkable, bool flyable, bool actionAfterMove,bool teleport, int moveX = 0,int moveY =0) : base(location,parent,walkable,flyable)
    {
        this.moveY = moveY;
        this.moveX = moveX;
        this.actionAfterMove = actionAfterMove;
        this.teleport = false;
        this.linkedX = location.x;
        this.linkedY = location.y;
    }
    public MovementTile((int x, int y) location, GameObject parent, bool walkable, bool flyable, bool actionAfterMove, bool teleport, int linkedX, int linkedY, int moveX = 0, int moveY = 0) : base(location, parent, walkable, flyable)
    {
        this.moveY = moveY;
        this.moveX = moveX;
        this.actionAfterMove = actionAfterMove;
        this.linkedX = linkedX;
        this.linkedY = linkedY;
        this.teleport = teleport;
    }

    public override void RepeatIntersect(Tile t)
    {
        Vector2 position = t.parent.transform.position;
        if (teleport)
        {
            if (GameManager.instance.active)
            {

                GameManager.instance.grid.MoveTileToLocation(location.x, location.y, linkedX, linkedY, t.id);
                t.location.y = linkedY;
                t.location.x = linkedX;
                position = GameManager.instance.grid.GetCentralWorldPosition(linkedX, linkedY);
                
                GameManager.instance.active = false;
            }
        }
        else
        {
            GameManager.instance.grid.MoveTile(location.x, location.y, moveX, moveY, t.id);
            t.location.x += moveX;
            t.location.y += moveY;
            position.y += moveY * GameManager.SIZE;
            position.x += moveX * GameManager.SIZE;
            //This should be a loop like bullet does for its movement.
        }
        if (actionAfterMove)
        {
            t.Collide();
        }
        t.parent.transform.position = position;
    }
    
}
