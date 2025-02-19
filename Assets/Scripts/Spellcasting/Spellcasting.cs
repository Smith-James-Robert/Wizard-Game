using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Spellcasting : MonoBehaviour
{
    [SerializeField]
    private GameObject damageOverTime;
    bool shift = false;
    bool ctrl = false;
    const int maxPrimaryElements = 3;
    const int maxSpells = 2;
    const int maxSecondaryElements = 3;
    const int maxShapes = 3;
    int currentPrimaryElements, currentSecondaryElements, currentShapes, currentSpells;
    private ActionPointManager actionPointManager;
    bool casting;
    public AudioSource spellcastingSound;
    public TMP_Text text;
    private char[] spellText;
    public enum elements
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
    public Shape[] shapes;
    public Element[] primaryElements;
    public Element[] secondaryElements;
    public Queue<Spell> spells;
    // Start is called before the first frame update
    void Start()
    {
        actionPointManager = GameManager.instance.gameObject.GetComponent<ActionPointManager>();
        text = GameObject.Find("SpellData").GetComponent<TMP_Text>();
        currentPrimaryElements = 0;
        currentSecondaryElements = 0;
        currentSpells = 0;
        currentShapes = 0;
        shapes = new Shape[maxShapes];
        primaryElements = new Element[maxPrimaryElements];
        secondaryElements = new Element[maxSecondaryElements];
        spells = new Queue<Spell>();
        spellText = new char[maxPrimaryElements + maxSecondaryElements + maxShapes];
        ResetSpellText();
    }
    void ResetSpellText()
    {
        for (int i=0;i<maxPrimaryElements+maxShapes+maxSecondaryElements;i++)
        {
            spellText[i] = '-';
        }
        updateText();
    }
    public void TakeAction(int apCost)
    {
        actionPointManager.PlayerAP = Mathf.Max(actionPointManager.PlayerAP - apCost, 0);
        CheckTurn();
    }
    private void CheckTurn()
    {
        if (actionPointManager.PlayerAP == 0)
        {
            GameManager.instance.currentTurn++;
            TurnStorage.currentTurn++;
            actionPointManager.PlayerAP = actionPointManager.MaxAP;
        }
    }
    void SetSpellText()
    {
        for (int i = 0; i < maxPrimaryElements; i++)
        {
            if (currentPrimaryElements>i)
            {
                if (primaryElements[i] != null)
                {
                    if (primaryElements[i].GetElement() == (int)elements.Fire)
                    {
                        spellText[i] = 'F';
                    }
                    else if (primaryElements[i].GetElement() == (int)elements.Water)
                    {
                        spellText[i] = 'W';
                    }
                    else if (primaryElements[i].GetElement() == (int)elements.Earth)
                    {
                        spellText[i] = 'E';
                    }
                    else if (primaryElements[i].GetElement() == (int)elements.Air)
                    {
                        spellText[i] = 'A';
                    }
                }
                else
                {
                    spellText[i] = '0';
                }
            }
            else
            {
                spellText[i]='-';
            }
        }
        for (int i = maxPrimaryElements; i < maxSecondaryElements + maxPrimaryElements; i++)
        {
            if (currentSecondaryElements+maxPrimaryElements>i)
            {
                if (secondaryElements[i-maxPrimaryElements].GetElement() == (int)elements.Fire)
                {
                    spellText[i] = 'F';
                }
                else if (secondaryElements[i-maxPrimaryElements].GetElement() == (int)elements.Water)
                {
                    spellText[i] = 'W';
                }
                else if (secondaryElements[i-maxPrimaryElements].GetElement() == (int)elements.Earth)
                {
                    spellText[i] = 'E';
                }
                else if (secondaryElements[i-maxPrimaryElements].GetElement() == (int)elements.Air)
                {
                    spellText[i] = 'A';
                }
            }
            else
            {
                spellText[i] = '-';
            }
        }
        //for (int i = maxPrimaryElements + maxSecondaryElements; i < maxShapes + maxPrimaryElements + maxSecondaryElements; i++)
        //{
        //    if (currentShapes+maxPrimaryElements+maxSecondaryElements>i)
        //    {
        //        if (shapes[i-maxPrimaryElements-maxSecondaryElements]==Shape.Arrow)
        //        {
        //            spellText[i] = 'A';
        //        }
        //        else if (shapes[i-maxSecondaryElements-maxSecondaryElements]== Shape.Step)
        //        {
        //            spellText[i] = 'S';
        //        }
        //        else if (shapes[i - maxSecondaryElements - maxSecondaryElements]== Shape.Line)
        //        {
        //            spellText[i] = 'L';
        //        }
        //        else if (shapes[i - maxSecondaryElements - maxSecondaryElements] == Shape.Burst)
        //        {
        //            spellText[i] = 'B';
        //        }


        //    }
        //}
        updateText();
    }
    void updateText()
    {
        text.text = "";
        for (int i = 0; i < maxPrimaryElements; i++)
        {
            text.text += spellText[i];
            text.text += " ";
        }
        text.text += '\n';
        for (int i = maxPrimaryElements; i < maxSecondaryElements + maxPrimaryElements; i++)
        {
            text.text += spellText[i];
            text.text += " ";
        }
        text.text += '\n';

        //for (int i = maxPrimaryElements + maxSecondaryElements; i < maxShapes + maxPrimaryElements + maxSecondaryElements; i++)
        //{
        //    text.text += spellText[i];
        //    text.text += " ";
        //}


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ctrl = true;
        }
        if( Input.GetKeyUp(KeyCode.LeftShift)) {
            ctrl = false;
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ctrl = true;
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            ctrl = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (shift)
            {
                if (currentShapes < maxShapes)
                {
                    shapes[currentShapes] = Shape.Arrow;
                    currentShapes++;
                }
            }
            else if (ctrl)
            {
                if (currentSecondaryElements < maxSecondaryElements)
                {
                    secondaryElements[currentSecondaryElements]= new Element((int)elements.Fire);
                    currentSecondaryElements++;
                    TakeAction(1);
                }
            }
            else
            {
                if (currentPrimaryElements < maxPrimaryElements)
                {
                    if (currentPrimaryElements == 0 || (primaryElements[0] != null && primaryElements[0].GetElement() == (int)elements.Fire))
                    {
                        primaryElements[currentPrimaryElements] = new Element((int)elements.Fire);
                        currentPrimaryElements++;
                        TakeAction(1);

                    }
                }
            }
            SetSpellText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (shift)
            {
                if (currentShapes < maxShapes)
                {
                    shapes[currentShapes] = Shape.Step;
                    currentShapes++;
                }
            }
            else if (ctrl)
            {
                if (currentSecondaryElements < maxSecondaryElements)
                {
                    secondaryElements[currentSecondaryElements] = new Element((int)elements.Air);
                    currentSecondaryElements++;
                    TakeAction(1);

                }
            }
            else
            {
                if (currentPrimaryElements < maxPrimaryElements)
                {
                    if (currentPrimaryElements == 0 || (primaryElements[0] != null && primaryElements[0].GetElement() == (int)elements.Air))
                    {
                        primaryElements[currentPrimaryElements] = new Element((int)elements.Air);

                        currentPrimaryElements++;
                        TakeAction(1);

                    }
                }
            }
            SetSpellText();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (shift)
            {
                shapes[currentShapes] = Shape.Step;
                currentShapes++;
            }
            else if (ctrl)
            {
                secondaryElements[currentSecondaryElements] = new Element((int)elements.Water);
                currentSecondaryElements++;
                TakeAction(1);

            }
            else
            {
                if (currentPrimaryElements < maxPrimaryElements)
                {
                    if (currentPrimaryElements == 0 || (primaryElements[0] != null && primaryElements[0].GetElement() == (int)elements.Water))
                    {
                        primaryElements[currentPrimaryElements] = new Element((int)elements.Water);
                        currentPrimaryElements++;
                        TakeAction(1);

                    }
                }
            }
            SetSpellText();

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (shift)
            {
                shapes[currentShapes] = Shape.Burst;
                currentShapes++;
            }
            else if (ctrl)
            {
                secondaryElements[currentSecondaryElements] = new Element((int)elements.Earth);
                currentSecondaryElements++;
                TakeAction(1);

            }
            else
            {
                if (currentPrimaryElements < maxPrimaryElements)
                {
                    if (currentPrimaryElements == 0 || (primaryElements[0] != null && primaryElements[0].GetElement() == (int)elements.Earth))
                    {
                        Debug.Log("Earth");
                        primaryElements[currentPrimaryElements] = new Element((int)elements.Earth);
                        currentPrimaryElements++;
                        TakeAction(1);
                    }

                }
            }
            SetSpellText();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ctrl)
            {
                Debug.Log("Cast Spell");
                if (currentSpells>0)
                {
                    GameManager.instance.playerCasting = true;
                    currentSpells--;
                    TakeAction(1);

                    //TakeAction(99);
                }
            }
            else if (shift)
            {
                Debug.Log("Shift Spells");
                if (currentSpells > 1)
                {
                    //Shift Spells
                    //Take Action(1);
                }
            }
            else
            {
                Debug.Log("Add Spell");
                CreateSpell();

                //CreateSpell();
                //TakeAction(99);
            }
        }
        if (GameManager.instance.playerCasting==true && casting==false)
        {
            casting = true;
            spellcastingSound.Play();
        }
        if (GameManager.instance.playerCasting==false)
        {
            casting = false;
            spellcastingSound.Stop();
        }
    }
    public void CastSpell(int x,int y)
    {
        //if (spells.Peek().NumShapes()>0)
        {
            //if ((int)spells.Peek().shapes[0]==(int)Shape.Arrow)
            {
                GameObject arrow = Resources.Load("Prefabs/SpellArrow") as GameObject;
                SpellArrow spellArrow;
                arrow = Instantiate(arrow);
                spellArrow=arrow.gameObject.GetComponent<SpellArrow>();
                spellArrow.startingX = GameManager.instance.player.location.x + x;
                spellArrow.startingY = GameManager.instance.player.location.y + y;
                spellArrow.damage=spells.Peek().primaryDamage;
                spellArrow.health=spells.Peek().primaryHealth;
                //duration?
                if (x==0)
                {
                    spellArrow.ySpeed = y*spells.Peek().primaryMobility;//multiply by mobility?
                }
                else
                {
                    spellArrow.xSpeed = x*spells.Peek().primaryMobility; ;
                }
                if (spells.Peek().primaryMobility==0) //2=Right,3=Up,1=Down,0=Left
                {
                   if (x==1)
                    {
                        spellArrow.direction = 2;
                    }
                   else if (x==-1)
                    {
                        spellArrow.direction = 0;
                    }
                   else if (y==1)
                    {
                        spellArrow.direction = 3;
                    }
                   else if (y==-1)
                    {
                        spellArrow.direction = 1;
                    }
                }
                Debug.Log("Spells.peek=" + spells.Peek().primaryElements[0]);
                spellArrow.SetSpell(spells.Peek());
                spells.Peek().SetSpellBase(spellArrow);
                spells.Dequeue();
            }
            //else if ((int)spells.Peek().shapes[0]==(int)Shape.Line)
            //{
            //    //
            //    SpellLine spellLine = new SpellLine();

            //    spellLine.health = spells.Peek().primaryHealth;
            //    spellLine.damage = spells.Peek().primaryDamage;
            //    spellLine.mobility = spells.Peek().primaryMobility;
            //    spellLine.SetDirection(x, y);
            //}
        }
    }
    public int NumShapes()
    {
        for (int i = 0; i < shapes.Length; i++)
        {
            if (shapes[i] == Shape.None)
            {
                return i;
            }
        }
        return 0;
    }
    private void CreateSpell()
    {
        if (currentSpells < maxSpells && currentPrimaryElements>0) //&& NumShapes()>0)
        {
            int[] passShape = new int[maxShapes];
            for (int i = 0; i < shapes.Length; i++)
            {
                passShape[i] = (int)shapes[i];
            }
            Debug.Log("Primary Element" + primaryElements[0].GetElement());
            spells.Enqueue(new Spell(primaryElements, secondaryElements, passShape,damageOverTime));
            currentShapes = 0;
            currentPrimaryElements = 0;
            currentSecondaryElements = 0;
            for (int i=0; i< maxPrimaryElements; i++)
            {
                primaryElements[i] = null;
            }
            for (int i=0; i< maxSecondaryElements; i++)
            {
                secondaryElements[i] = null;
            }
            //for (int i=0; i < maxShapes; i++)
            //{
            //    shapes[i]=Shape.None;
            //}
            currentSpells++;
            ResetSpellText();
        }
    }
}
