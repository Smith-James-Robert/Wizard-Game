using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLine : Spellbase
{
    // Start is called before the first frame update
    public int damage;
    public int direction;
    public int health;
    public int mobility;
    public int startingX, startingY;
    private SpellLineBehaviour behaviour=null;
    private Spell spell;
    public override void SetSpell(Spell spell)
    {
        if (behaviour == null)
        {
            //behaviour = new SpellLineBehaviour((startingX, startingY), this.gameObject, true, true, damage, true);
        }
        //behaviour.spell = spell;
        this.spell = spell;
    }
     //2=Right,3=Up,1=Down,0=Left

    public void SetDirection(int x, int y)
    {
        if (x>0)
        {
            direction = 2;
        }
        else if (x<0)
        {
            direction = 0;
        }
        else if (y<0)
        {
            direction = 1;
        }
        else if (y>0)
        {
            direction = 3;
        }
        else
        {
            direction = -1;
        }
    }
    public override int GetDirection()
    {
        return direction;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
