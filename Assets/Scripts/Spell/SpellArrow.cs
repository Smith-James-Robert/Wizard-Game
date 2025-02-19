using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellArrow : Spellbase
{
    public int damage;
    public int xSpeed;
    public int ySpeed;
    public int startingX, startingY;
    public int health;
    public int direction=-1;
    private SpellArrowBehaviour behaviour=null;
    [SerializeField]
    private OwnTime ownTime;
    private Spell spell;
    public override void SetSpell(Spell spell)
    {
        if (behaviour == null)
        {
            behaviour = new SpellArrowBehaviour((startingX, startingY), this.gameObject, true, true, damage, xSpeed, ySpeed, true,health);
        }
        behaviour.spell = spell;
        this.spell = spell;
    }
    public override int GetDirection() //2=Right,3=Up,1=Down,0=Left
    {
        if (direction == -1)
        {
            if (xSpeed > 0)
            {
                return 2;
            }
            else if (ySpeed > 0)
            {
                return 3;
            }
            else if (ySpeed < 0)
            {
                return 1;
            }
            else if (xSpeed < 0)
            {
                return 0;
            }
            else return -1;
        }
        else
        {
            return direction;
        }
    }
    private void Start()
    {
        if (behaviour == null)
        {
            behaviour = new SpellArrowBehaviour((startingX, startingY), this.gameObject, true, true, damage, xSpeed, ySpeed, true,health);
        }
        this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(startingX, startingY);
        GameManager.instance.grid.AddValue(startingX, startingY, behaviour);
        Vector3 position = this.transform.position;
        position.z = 0;
        this.transform.position = position;
        this.transform.Rotate(0, 0, GetDirection() * 90);
        ownTime = gameObject.GetComponent<OwnTime>();
        behaviour.Collide();
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
            for (int i = 0; i < Mathf.Max(Mathf.Abs(xSpeed), Mathf.Abs(ySpeed)); i++)
            {
                if (Mathf.Abs(xSpeed) > i)
                {
                    speedDirection = xSpeed / Mathf.Abs(xSpeed);
                    GameManager.instance.grid.MoveTile(behaviour.location.x, behaviour.location.y, speedDirection, 0, behaviour.id);
                    position.x += (xSpeed / Mathf.Abs(xSpeed)) * GameManager.SIZE;
                    behaviour.location.x += speedDirection;
                    behaviour.Collide();
                }
                if (Mathf.Abs(ySpeed) > i)
                {
                    speedDirection = ySpeed / Mathf.Abs(ySpeed);
                    GameManager.instance.grid.MoveTile(behaviour.location.x, behaviour.location.y, 0, speedDirection, behaviour.id);
                    position.y += (ySpeed / Mathf.Abs(ySpeed)) * GameManager.SIZE;
                    behaviour.location.y += speedDirection;
                    behaviour.Collide();

                }
            }
            transform.position = position;
            ownTime.UpdateTurn();
        }
    }
}

