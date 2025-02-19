using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public MeleeCharacter behaviour;
    public int startingX, startingY, damage, speed;
    public OwnTime ownTime;
    public Pathfinding pathfinding;
    public Health health;
    private Health playerHealth;
    [SerializeField]
    bool flying;
    void Start()
    {
        behaviour = new MeleeCharacter((startingX, startingY), this.gameObject, false, false, damage, flying);
        this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(startingX, startingY);
        GameManager.instance.grid.AddValue(startingX, startingY, behaviour);
        Vector3 position = this.transform.position;
        position.z = 0;
        this.transform.position = position;
        ownTime = gameObject.GetComponent<OwnTime>();
        pathfinding = new Pathfinding();
        playerHealth = GameManager.instance.player.parent.GetComponent<Health>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ownTime.currentTurn < GameManager.instance.currentTurn)
        {
                int actions = speed;
                while (actions > 0)
                {
                    FindPath();
                    if (pathfinding.paths!=null && pathfinding.paths.Count > 0)
                    {
                        Vector2 position = gameObject.transform.position;
                        GameManager.instance.grid.MoveTileToLocation(behaviour.location.x, behaviour.location.y, pathfinding.paths[0].location.x, pathfinding.paths[0].location.y, behaviour.id);
                        actions -= GameManager.instance.grid.GetDifficulty(pathfinding.paths[0].location.x, pathfinding.paths[0].location.y);
                        position.x += (pathfinding.paths[0].location.x - behaviour.location.x)* GameManager.SIZE;
                        position.y += (pathfinding.paths[0].location.y - behaviour.location.y)* GameManager.SIZE;
                        behaviour.location.x = pathfinding.paths[0].location.x;
                        behaviour.location.y = pathfinding.paths[0].location.y;
                        behaviour.Collide();
                        transform.position = position;
                        pathfinding.paths.RemoveAt(0);
                    }
                    else
                    {
                        Attack();
                        actions--;
                    }
                }
                this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(behaviour.location.x, behaviour.location.y);
            ownTime.UpdateTurn();
            Debug.Log(behaviour.location);
        }
    }
    public void OnDestroy()
    {
        
         GameManager.instance.grid.RemoveValue(behaviour.location.x, behaviour.location.y, behaviour);
    }
    public void Attack()
    {
        playerHealth.changeHealth(-damage);
    }
    public bool FindPath() //1=right, 2=left, 3= top, 4= bottom
    {

        int shortestDirection = 0;
        int shortestLength = int.MaxValue;
        GameManager.instance.GetPlayer();
        pathfinding.FindPath(behaviour.location.x, behaviour.location.y, GameManager.instance.player.location.x + 1, GameManager.instance.player.location.y, flying);
        if (behaviour.location.x == GameManager.instance.player.location.x+1 && behaviour.location.y == GameManager.instance.player.location.y)
        {
            //pathfinding.paths.count
        }
        if (pathfinding.paths != null)
        {
            if (pathfinding.paths.Count == 0)
            {
                shortestDirection = 1;
                shortestLength = 0;
            }
            if (pathfinding.paths.Count > 0)
            {
                shortestLength = pathfinding.paths[pathfinding.paths.Count - 1].trueCostToEnd;
                shortestDirection = 1;
            }
        }
        pathfinding.FindPath(behaviour.location.x, behaviour.location.y, GameManager.instance.player.location.x - 1, GameManager.instance.player.location.y, flying);
        if (pathfinding.paths != null)
        {
            if (pathfinding.paths.Count == 0)
            {
                shortestDirection = 2;
                shortestLength = 0;

            }
            if (pathfinding.paths.Count > 0)
            {
                if (shortestLength > pathfinding.paths[pathfinding.paths.Count - 1].trueCostToEnd)
                {
                    shortestDirection = 2;
                    shortestLength = pathfinding.paths[pathfinding.paths.Count - 1].trueCostToEnd;
                }
            }
        }
        pathfinding.FindPath(behaviour.location.x, behaviour.location.y, GameManager.instance.player.location.x, GameManager.instance.player.location.y + 1, flying);
        if (pathfinding.paths != null)
        {
            if (pathfinding.paths.Count ==0)
            {
                shortestDirection = 3;
                shortestLength = 0;

            }
            if (pathfinding.paths.Count > 0)
            {
                if (shortestLength > pathfinding.paths[pathfinding.paths.Count - 1].trueCostToEnd)
                {
                    shortestDirection = 3;
                    shortestLength = pathfinding.paths[pathfinding.paths.Count - 1].trueCostToEnd;
                }
            }
        }
        pathfinding.FindPath(behaviour.location.x, behaviour.location.y, GameManager.instance.player.location.x, GameManager.instance.player.location.y - 1, flying);
        if (pathfinding.paths!=null)
        {
            if (pathfinding.paths.Count == 0)
            {
                shortestDirection = 4;
                shortestLength = 0;

            }
            if (pathfinding.paths.Count > 0)
            {
                if (shortestLength > pathfinding.paths[pathfinding.paths.Count - 1].trueCostToEnd)
                {
                    shortestDirection = 4;
                    shortestLength = pathfinding.paths[pathfinding.paths.Count - 1].trueCostToEnd;
                }
            }
        }

        if (shortestDirection == 1)
        {
            pathfinding.FindPath(behaviour.location.x, behaviour.location.y, GameManager.instance.player.location.x + 1, GameManager.instance.player.location.y, flying);
        }
        else if (shortestDirection == 2)
        {
            pathfinding.FindPath(behaviour.location.x, behaviour.location.y, GameManager.instance.player.location.x - 1, GameManager.instance.player.location.y, flying);

        }
        else if (shortestDirection == 3)
        {
            pathfinding.FindPath(behaviour.location.x, behaviour.location.y, GameManager.instance.player.location.x, GameManager.instance.player.location.y + 1, flying);

        }
        else if (shortestDirection == 0)
        {
            Debug.Log("No Path!");
            return false;
        }
        //Debug.Log("if pathfinding.findPath" + pathfinding.FindPath(behaviour.location.x, behaviour.location.y, GameManager.instance.player.location.x, GameManager.instance.player.location.y + 1, false) == null);
        return true;
    }
}
