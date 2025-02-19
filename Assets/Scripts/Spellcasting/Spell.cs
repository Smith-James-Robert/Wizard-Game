using UnityEngine;

public class Spell
{
    public GameObject damageOverTimeGameObject;
    private DamageOverTime damageOverTime;
    private Spellbase parent;
    const int maxPrimaryElements = 3;
    const int maxSpells = 2;
    const int maxSecondaryElements = 3;
    const int maxShapes = 1;
    public int primaryDamage, secondaryDamage;
    public int primaryMobility, secondaryMobility;
    public int primaryHealth, secondaryHealth;
    public Element[] primaryElements,secondaryElements;
    public Shape[] shapes;
    private enum Elements
    {
        Fire,
        Water,
        Earth,
        Air
    }
    public enum Shape
    {
        None,
        Arrow,
        Burst,
        Line,
        Step
    }
    public int NumShapes()
    {
        for (int i = 0; i < shapes.Length; i++)
        {
            if (shapes[i]==Shape.None)
            {
                return i;
            }
        }
        return shapes.Length;
    }
    //public Spell(Spell spell)
    //{
    //    this.damageOverTimeGameObject = spell.damageOverTimeGameObject;
    //    for (int i = 0; i < maxPrimaryElements; i++)
    //    {
    //        if (primaryElements[i] != null)
    //        {
    //            this.primaryElements[i] = new Element(spell.primaryElements[i]);
    //        }
    //    }
    //    for (int i = 0; i < maxSecondaryElements; i++)
    //    {
    //        if (secondaryElements[i] != null)
    //        {
    //            this.secondaryElements[i] = new Element(spell.secondaryElements[i]);
    //        }
    //        //Debug.Log("Secondary Element #" + i + " : " + secondaryElements[i].GetElement());
    //    }
    //}
    public Spell(Element[] primaryElements, Element[] secondaryElements, int[] shapes, GameObject damageOverTime)
    {
        Debug.Log("Creating New Spell");
        this.damageOverTimeGameObject = damageOverTime;
        this.primaryElements = new Element[maxPrimaryElements];
        for (int i = 0; i < maxPrimaryElements; i++)
        {
            Debug.Log("Element #" + i + "=");
            if (primaryElements[i] != null)
            {
                Debug.Log(primaryElements[i].GetElement());
                this.primaryElements[i] = new Element(primaryElements[i]);
            }
            else
            {
                Debug.Log("Null");
            }
        }
        this.secondaryElements = new Element[maxSecondaryElements];
        for (int i = 0; i < maxSecondaryElements; i++)
        {
            if (secondaryElements[i] != null)
            {
                this.secondaryElements[i] = new Element(secondaryElements[i]);
            }
            //Debug.Log("Secondary Element #" + i + " : " + secondaryElements[i].GetElement());
        }
        this.shapes = new Shape[maxShapes];
        for (int i = 0; i < maxShapes && i < shapes.Length; i++)
        {
            this.shapes[i] = (Shape)shapes[i];
        }
        CreateSpell();
    }
    public void SetSpellBase(Spellbase spellbase)
    {
        parent = spellbase;
    }
    //    public Spell(Element[] primaryElements, Element[] secondaryElements, int[] shapes, GameObject damageOverTime, Spellbase parent)
    //{
    //    this.damageOverTimeGameObject = damageOverTime;
    //    this.primaryElements = primaryElements;
    //    for (int i = 0; i < maxPrimaryElements; i++)
    //    {
    //        this.primaryElements[i] = primaryElements[i];
    //    }
    //    this.secondaryElements = secondaryElements;
    //    for (int i = 0; i < maxSecondaryElements; i++)
    //    {
    //        this.secondaryElements[i] = secondaryElements[i];
    //    }
    //    this.shapes = new Shape[maxShapes];
    //    for (int i = 0; i < maxShapes && i < shapes.Length; i++)
    //    {
    //        this.shapes[i] = (Shape)shapes[i];
    //    }
    //    CreateSpell();
    //    this.parent = parent;
    //}
    public void CreateSpell()
    {
        int spellDamage = 0, spellHealth = 0, spellMobility = 0;

        if (primaryElements.Length > 0)
        {
            for (int i = 0; i < primaryElements.Length; i++)
            {
                if (primaryElements[i] != null)
                {
                    spellDamage += primaryElements[i].GetPrimaryDamage();
                    spellMobility += primaryElements[i].GetPrimaryMobility();
                    spellHealth += primaryElements[i].GetPrimaryHealth();
                }
            }
            spellDamage /= 3; spellHealth /= 3; spellMobility /= 3;
        }
        primaryDamage = spellDamage; primaryHealth = spellHealth; primaryMobility = spellMobility;
    }
    private void SpellShape()
    {
        //if spellshape is arrow

    }
    public void SpellEffects(Tile t)
    {
        int numFire=0, numWater=0, numEarth=0, numAir=0;
        //if (NumShapes()>0)
        {
            //Debug.Log(shapes[0]);
            //if (shapes[0]==Shape.Arrow)
            {
                for (int i=0; i < secondaryElements.Length; i++)
                {
                    if (secondaryElements[i] != null)
                    {
                        Debug.Log(secondaryElements[i]);
                        if (secondaryElements[i].GetElement()==(int)Elements.Fire)
                        {
                            numFire++;
                        }
                        if (secondaryElements[i].GetElement()==(int)Elements.Water)
                        {
                            numWater++;
                        }
                        if (secondaryElements[i].GetElement()==(int)Elements.Earth)
                        {
                            numEarth++;
                        }
                        if (secondaryElements[i].GetElement()==(int) Elements.Air)
                        {
                            numAir++;
                        }
                    }
                }
                if (numFire>0)
                {
                    int fireDamageOverTime = numFire*2;
                    int fireDuration = (numFire * 3);
                    Health health=t.parent.GetComponent<Health>();
                    if (health != null)
                    {
                        Debug.Log("Has Health");
                        damageOverTimeGameObject = Object.Instantiate(damageOverTimeGameObject);
                        damageOverTime = damageOverTimeGameObject.GetComponent<DamageOverTime>();
                        damageOverTime.Initialize(fireDamageOverTime, fireDuration, GameManager.instance.currentTurn, t.parent.GetComponent<Health>());
                    }

                }
                if (numAir>0) //2=Right,3=Up,1=Down,0=Left
                {
                    int pushDistance = numAir*2;
                    if (parent.GetDirection()==0)
                    {
                        for (int i = 0; i < pushDistance; i++)
                        {
                            if (t.flying)
                            {
                                if (GameManager.instance.grid.GetFlyable(t.location.x-1, t.location.y))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, -1, 0, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.x--;
                                    position.x -= GameManager.SIZE;
                                    t.parent.transform.position = position;

                                }
                            }
                            else
                            {
                                if (GameManager.instance.grid.GetWalkable(t.location.x-1,t.location.y))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, -1, 0, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.x--;
                                    position.x -= GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                        }
                    }
                    else if (parent.GetDirection()==1)
                    {
                        for (int i = 0; i < pushDistance; i++)
                        {
                            if (t.flying)
                            {
                                if (GameManager.instance.grid.GetFlyable(t.location.x, t.location.y-1))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 0, -1, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.y--;
                                    position.y -= GameManager.SIZE;
                                    t.parent.transform.position = position;

                                }
                            }
                            else
                            {
                                if (GameManager.instance.grid.GetWalkable(t.location.x, t.location.y-1))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 0,-1, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.y--;
                                    position.y -= GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                        }
                    }
                    else if (parent.GetDirection()==2)
                    {
                        for (int i = 0; i < pushDistance; i++)
                        {
                            if (t.flying)
                            {
                                if (GameManager.instance.grid.GetFlyable(t.location.x + 1, t.location.y))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 1, 0, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.x++;
                                    position.x += GameManager.SIZE;
                                    t.parent.transform.position = position;

                                }
                            }
                            else
                            {
                                if (GameManager.instance.grid.GetWalkable(t.location.x + 1, t.location.y))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 1, 0, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.x++;
                                    position.x += GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                        }
                    }
                    else if (parent.GetDirection()==3)
                    {
                        for (int i = 0; i < pushDistance; i++)
                        {
                            if (t.flying)
                            {
                                if (GameManager.instance.grid.GetFlyable(t.location.x, t.location.y + 1))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 0, 1, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.y++;
                                    position.y += GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                            else
                            {
                                if (GameManager.instance.grid.GetWalkable(t.location.x, t.location.y + 1))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 0, 1, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.y++;
                                    position.y += GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                        }
                    }
                }
                if (numWater>0)
                {
                    int pullDistance = numWater*3; // Pulling is stronger because right now its harder to make use of.
                    if (parent.GetDirection() == 0)
                    {
                        for (int i = 0; i < pullDistance; i++)
                        {
                            if (t.flying)
                            {
                                if (GameManager.instance.grid.GetFlyable(t.location.x + 1, t.location.y))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 1, 0, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.x++;
                                    position.x += GameManager.SIZE;
                                    t.parent.transform.position = position;

                                }
                            }
                            else
                            {
                                if (GameManager.instance.grid.GetWalkable(t.location.x + 1, t.location.y))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 1, 0, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.x++;
                                    position.x += GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                        }
                    }
                    else if (parent.GetDirection() == 1)
                    {
                        for (int i = 0; i < pullDistance; i++)
                        {
                            if (t.flying)
                            {
                                if (GameManager.instance.grid.GetFlyable(t.location.x, t.location.y + 1))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 0, 1, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.y++;
                                    position.y += GameManager.SIZE;
                                    t.parent.transform.position = position;

                                }
                            }
                            else
                            {
                                if (GameManager.instance.grid.GetWalkable(t.location.x, t.location.y + 1))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 0, 1, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.y++;
                                    position.y += GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                        }
                    }
                    else if (parent.GetDirection() == 2)
                    {
                        for (int i = 0; i < pullDistance; i++)
                        {
                            if (t.flying)
                            {
                                if (GameManager.instance.grid.GetFlyable(t.location.x - 1, t.location.y))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, -1, 0, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.x--;
                                    position.x -= GameManager.SIZE;
                                    t.parent.transform.position = position;

                                }
                            }
                            else
                            {
                                if (GameManager.instance.grid.GetWalkable(t.location.x - 1, t.location.y))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, -1, 0, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.x--;
                                    position.x -= GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                        }
                    }
                    else if (parent.GetDirection() == 3)
                    {
                        for (int i = 0; i < pullDistance; i++)
                        {
                            if (t.flying)
                            {
                                if (GameManager.instance.grid.GetFlyable(t.location.x, t.location.y - 1))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 0, -1, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.y--;
                                    position.y -= GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                            else
                            {
                                if (GameManager.instance.grid.GetWalkable(t.location.x, t.location.y - 1))
                                {
                                    GameManager.instance.grid.MoveTile(t.location.x, t.location.y, 0, -1, t.id);
                                    Vector2 position = t.parent.transform.position;
                                    t.location.y--;
                                    position.y -= GameManager.SIZE;
                                    t.parent.transform.position = position;
                                }
                            }
                        }
                    }
                }
                if (numEarth>0)
                {
                    GameObject mud = Resources.Load("Prefabs/TimedMud") as GameObject;
                    TimedMud mudValues;
                    mud =  GameObject.Instantiate(mud);
                    mudValues = mud.GetComponent<TimedMud>();
                    mudValues.startingX = t.location.x;
                    mudValues.startingY = t.location.y;
                    mudValues.duration = numEarth*5;

                }
            }
        }
        //
        //
        //
        //
    }
    public void CastSpell()
    {
        if (shapes.Length>0)
        {
            if (shapes[0]==Shape.Arrow)
            {

            }
        }
    }
}
