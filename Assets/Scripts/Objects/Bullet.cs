using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int xSpeed;
    public int ySpeed;
    public int startingX, startingY;
    private BulletBehaviour behaviour;
    private OwnTime ownTime;
    private void Start()
    {

        behaviour = new BulletBehaviour((startingX, startingY),this.gameObject,true,true,damage,xSpeed,ySpeed,true);
        this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(startingX,startingY);
        GameManager.instance.grid.AddValue(startingX, startingY,behaviour);
        Vector3 position = this.transform.position;
        position.z = 0;
        this.transform.position = position;
        int rotation=0;
        if (xSpeed>0)
        {
            rotation = 2;
        }
        else if (ySpeed>0)
        {
            rotation = 3;
        }
        else if (ySpeed<0)
        {
            rotation = 1;
        }
        this.transform.Rotate(0, 0, rotation*90);
        ownTime = gameObject.GetComponent<OwnTime>();
    }
    private void Intersect(Tile gameObject)
    {

        Health health = gameObject.parent.GetComponent<Health>();
        if (health)
        {
            health.changeHealth(-damage);
            Destroy(this.gameObject);
        }
        else if (gameObject.flyable == false)
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (ownTime.currentTurn < GameManager.instance.currentTurn)
        {
            int speedDirection;
            Vector2 position = gameObject.transform.position;
            for (int i = 0; i < Mathf.Max(Mathf.Abs(xSpeed),Mathf.Abs(ySpeed)); i++)
            {
                if (Mathf.Abs(xSpeed)>i)
                {
                    speedDirection=xSpeed/ Mathf.Abs(xSpeed);
                    GameManager.instance.grid.MoveTile(behaviour.location.x, behaviour.location.y, speedDirection, 0, behaviour.id);
                    position.x += (xSpeed / Mathf.Abs(xSpeed))*GameManager.SIZE;
                    behaviour.location.x += speedDirection;
                    behaviour.Collide();
                }
                if (Mathf.Abs(ySpeed)>i)
                {
                    speedDirection = ySpeed / Mathf.Abs(ySpeed);
                    GameManager.instance.grid.MoveTile(behaviour.location.x, behaviour.location.y, 0, speedDirection, behaviour.id);
                    position.y += (ySpeed / Mathf.Abs(ySpeed)) * GameManager.SIZE;
                    behaviour.location.y+= speedDirection;
                    behaviour.Collide();

                }
            }
            transform.position = position;
            ownTime.UpdateTurn();
        }
    }
}
