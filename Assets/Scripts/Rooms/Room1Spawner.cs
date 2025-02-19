using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room1Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject arrow;
    private GameObject working;
    public GameObject floorTrap;
    public GameObject portalDoor;
    public GameObject fireGuy;
    public GameObject wall;
    private Bullet bullet;
    private Spike spike;
    private Portal portal;
    private Wall wallTile;
    private Mud mudTile;
    public GameObject mud;
    bool once = true;
    void Start()
    {
        
        working = Instantiate(player);
        Debug.Log(working);
        GameManager.instance.playerObject = player;
        Debug.Log(GameManager.instance.playerObject);
        GameManager.instance.playerObject = working;
        Debug.Log(GameManager.instance.playerObject);
        //working =Instantiate(arrow);
        //bullet =working.gameObject.GetComponent<Bullet>();
        //bullet.startingY = 7;
        ////GameManager.instance.grid.MoveTile(5, 5, 3, 1, 1);
        //Instantiate(arrow);
        //for (int i = 0; i< 10; i++)
        //{
        //    working = Instantiate(floorTrap);
        //    spike=working.gameObject.GetComponent<Spike>();
        //    spike.startingY = i;
        //    GameManager.instance.grid.MoveTile(5, 0, 5, i, i + 1);
        //    if (i ==4)
        //    {
        //        i = 6;
        //    }
        ////}
        //Instantiate(portalDoor);
        //working = Instantiate(portalDoor);
        //portal=working.gameObject.GetComponent<Portal>();
        //(portal.startingY, portal.linkedY) = (portal.linkedY, portal.startingY);
        //(portal.startingX, portal.linkedX) = (portal.linkedX, portal.startingX);
        //Instantiate(mud);
        //Instantiate(fireGuy);
        for (int i=GameManager.HEIGHT; i > 10;i--)
        {
           
        }
        for (int i=0; i < 15;i++)
        {
            if (i==7)
            {
                i++;
            }
            working=Instantiate(wall);
            wallTile=working.gameObject.GetComponent<Wall>();
            wallTile.startingY = GameManager.HEIGHT - 6;
            wallTile.startingX = i;

        }
        for (int i=0;i< 6;i++)
        {
            working = Instantiate(floorTrap);
            spike = working.gameObject.GetComponent<Spike>();
            spike.startingX = 15;
            spike.startingY = GameManager.HEIGHT - i;
        }
        
        for (int i=GameManager.HEIGHT/2-2; i < (GameManager.HEIGHT/2)+2;i++)
        {
            for (int j = 0; j < 4; j++)
            {
                working = Instantiate(floorTrap);
                spike = working.gameObject.GetComponent<Spike>();
                spike.startingX = (GameManager.WIDTH / 2) + j;
                spike.startingY = i;
            }
        }
        for (int i=-4;i<4;i++)
        {
            working=Instantiate(wall);
            wallTile=working.gameObject.GetComponent<Wall>();
            wallTile.startingX = (GameManager.WIDTH / 2) + 4;
            wallTile.startingY = (GameManager.HEIGHT / 2) + i;
        }
        for (int i = 0; i < 5; i++)
        {
            working = Instantiate(wall);
            wallTile=working.gameObject.GetComponent<Wall>() ;
            wallTile.startingX = (GameManager.WIDTH / 2)+6 + i;
            wallTile.startingY = (GameManager.HEIGHT) - 4;

        }
        for (int i=0; i < 5; i++)
        {
            working = Instantiate(wall);
            wallTile = working.gameObject.GetComponent<Wall>();
            wallTile.startingX = (GameManager.WIDTH / 2)+6 + i;
            wallTile.startingY = (GameManager.HEIGHT) - 6;
        }
        working = Instantiate(mud);
        mudTile = working.gameObject.GetComponent<Mud>();
        mudTile.startingX = (GameManager.WIDTH / 2)+6;
        mudTile.startingY = GameManager.HEIGHT - 5;

        working = Instantiate(mud);
        mudTile = working.gameObject.GetComponent<Mud>();
        mudTile.startingX = (GameManager.WIDTH / 2)+10;
        mudTile.startingY = GameManager.HEIGHT - 5;
        int bottomMud = 0;
        for (int i=0;i<13;i++)
        {
            working = Instantiate(mud);
            mudTile = working.gameObject.GetComponent<Mud>();
            mudTile.startingX = i;
            mudTile.startingY = 6 - bottomMud;
            if (i > 5)
            {
                bottomMud++;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            working = Instantiate(floorTrap);
            spike = working.gameObject.GetComponent<Spike>();
            spike.startingX = 30;
            spike.startingY = i;
        }
        working = Instantiate(wall);
        wallTile = working.gameObject.GetComponent<Wall>();
        wallTile.startingX = 30;
        wallTile.startingY = 4;
        working = Instantiate(wall);
        wallTile = working.gameObject.GetComponent<Wall>();
        wallTile.startingX = 30;
        wallTile.startingY = 5;



    }

    // Update is called once per frame
    void Update()
    {

    }
}
